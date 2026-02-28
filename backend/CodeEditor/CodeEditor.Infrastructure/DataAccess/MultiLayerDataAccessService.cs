using CodeEditor.Domain.DataAccess;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Specifications;
using MessagePack;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace CodeEditor.Infrastructure.DataAccess
{
    public class MultiLayerDataAccessService<T> where T : class, IMultiLayerDataAccessService<T>
    {

        private readonly IRepository<T> _repository;
        private readonly IDatabase _redisCache;
        private readonly IMemoryCache _memoryCache;

        private readonly TimeSpan _memoryCacheTtl = TimeSpan.FromMinutes(5);
        private readonly TimeSpan _redisCacheTtl = TimeSpan.FromMinutes(30);

        public MultiLayerDataAccessService(
            IRepository<T> repository,
            IDatabase redisCache,
            IMemoryCache memoryCache)
        {
            _repository = repository;
            _redisCache = redisCache;
            _memoryCache = memoryCache;
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
