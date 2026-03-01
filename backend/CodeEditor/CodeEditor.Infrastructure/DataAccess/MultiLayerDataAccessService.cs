using CodeEditor.Domain.DataAccess;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Specifications;
using MessagePack;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Runtime.CompilerServices;

namespace CodeEditor.Infrastructure.DataAccess
{
    public class MultiLayerDataAccessService<T> : IMultiLayerDataAccessService<T> where T : class
    {
        private readonly ILogger<MultiLayerDataAccessService<T>> _logger;

        private readonly IRepository<T> _repository;
        private readonly IDatabase _redisCache;
        private readonly IMemoryCache _memoryCache;

        private readonly TimeSpan _memoryCacheTtl = TimeSpan.FromMinutes(5);
        private readonly TimeSpan _redisCacheTtl = TimeSpan.FromMinutes(30);

        public MultiLayerDataAccessService(
            IRepository<T> repository,
            IDatabase redisCache,
            IMemoryCache memoryCache,
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
            if (!_memoryCache.TryGetValue(entityKey, out entity))
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

                    return entity;
                }

                await SetEntityValueInMemory(entityName, id, entity!);

                entity = MessagePackSerializer.Deserialize<T>(redisObject);
                return entity;
            }

            return entity;
        }

        public async Task<List<T>?> GetMultipleEntityValue(
            string entityName, 
            List<long> ids, 
            Specification<T> spec)
        {
            var listRemainingEntities = new List<long>(ids);
            List<T> results = new List<T>();   

            foreach (var id in ids)
            {
                if(_memoryCache.TryGetValue(entityName + ":" + id, out T? entity))
                {
                    results.Add(entity!);
                    listRemainingEntities.Remove(id);
                }
            }

            ids = new List<long>(listRemainingEntities);

            foreach (var id in ids) {
                var redisValue = await _redisCache.StringGetAsync(entityName + ":" + id);
                if (redisValue != RedisValue.Null)
                {
                    T? entity = MessagePackSerializer.Deserialize<T>(redisValue);
                    results.Add(entity!);
                    listRemainingEntities.Remove(id);
                }
            }

            ids = new List<long>(listRemainingEntities);

            foreach (var id in ids)
            {
                T? entity = await _repository.FindOneAsync(spec);
                if (entity != null)
                {
                    results.Add(entity!);
                    listRemainingEntities.Remove(id);
                }
            }

            if(listRemainingEntities.Count > 0)
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
            return _memoryCache.Set(key, entity, _memoryCacheTtl) != null;
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
