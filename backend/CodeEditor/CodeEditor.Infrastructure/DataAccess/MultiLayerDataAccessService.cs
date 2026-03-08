using CodeEditor.Domain.DataAccess;
using CodeEditor.Domain.Entities.Base;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Specifications;
using MessagePack;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CodeEditor.Infrastructure.DataAccess
{
    public class MultiLayerDataAccessService<T> : IMultiLayerDataAccessService<T> where T : Entity
    {
        private readonly ILogger<MultiLayerDataAccessService<T>> _logger;

        private readonly IRepository<T> _repository;
        private readonly IDatabase _redisCache;
        private readonly IInMemoryCache _memoryCache;

        private readonly TimeSpan _memoryCacheTtl = TimeSpan.FromMinutes(15);
        private readonly TimeSpan _redisCacheTtl = TimeSpan.FromHours(1);

        public MultiLayerDataAccessService(
            IRepository<T> repository,
            IDatabase redisCache,
            IInMemoryCache memoryCache,
            ILogger<MultiLayerDataAccessService<T>> logger)
        {
            _repository = repository;
            _redisCache = redisCache;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<T?> GetEntityValue(
            string entityName,
            long id,
            Specification<T> spec)
        {
            T? entity;
            var entityKey = string.Concat(entityName, ":", id);

            entity = _memoryCache.GetEntity<T>(entityKey);
            if (entity == null)
            {
                var redisObject = await _redisCache.StringGetAsync(entityKey);

                if (redisObject == RedisValue.Null)
                {
                    entity = await _repository.FindOneAsync(spec);
                    if (entity != null)
                    {
                        await SetEntityValueInMemory(entityName, id, entity!);
                        await SetEntityValueRedis(entityName, id, entity!);
                    }

                    entity = MessagePackSerializer.Deserialize<T>(redisObject);
                    return entity;
                }

                await SetEntityValueInMemory(entityName, id, entity!);

                return entity;
            }

            return entity;
        }

        public async Task<List<T>?> GetMultipleEntityValue(
            string entityName,
            List<long> ids,
            Func<List<long>, Specification<T>> spec)
        {
            var listRemainingEntities = new List<long>(ids);
            List<T> results = new List<T>();

            foreach (var id in ids)
            {
                T? entity = _memoryCache.GetEntity<T>(entityName + ":" + id);
                if (entity == null)
                    continue;

                results.Add(entity!);
                listRemainingEntities.Remove(id);
            }

            ids = [.. listRemainingEntities];

            foreach (var id in ids)
            {
                var redisValue = await _redisCache.StringGetAsync(entityName + ":" + id);
                if (redisValue != RedisValue.Null)
                {
                    T entity = MessagePackSerializer.Deserialize<T>(redisValue);

                    results.Add(entity!);
                    listRemainingEntities.Remove(id);

                    await SetEntityValueInMemory(entityName, id, entity);
                }
            }

            ids = [.. listRemainingEntities];

            List<T>? entites = await _repository.FindAllAsync(spec(ids)) as List<T>;
            if (entites != null
                && entites.Count != 0)
            {
                foreach (T entity in entites)
                {
                    results.Add(entity!);
                    listRemainingEntities.Remove(entity.Id);

                    await SetEntityValueInMemory(entityName, entity.Id, entity);
                    await SetEntityValueRedis(entityName, entity.Id, entity);
                }
            }

            if (listRemainingEntities.Count > 0)
            {
                _logger.LogWarning("GetMultipleEntityValue - Not all entities were found - Not Found {ListRemainingEntities}", listRemainingEntities.ToString());
            }

            return results;
        }

        public async Task<bool> SetEntityValueInMemory(
            string entityName,
            long id,
            T entity)
        {
            var key = string.Concat(entityName, ":", id);
            return _memoryCache.SetEntity(key, entity, _memoryCacheTtl);
        }

        public async Task<bool> SetEntityValueRedis(
            string entityName,
            long id,
            T entity)
        {
            var key = string.Concat(entityName, ":", id);
            var result = await _redisCache.SetAddAsync(key, MessagePackSerializer.Serialize(entity));
            var resultTtl = await _redisCache.KeyExpireAsync(key, _redisCacheTtl);
            return result && resultTtl;
        }
    }
}
