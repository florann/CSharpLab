using CodeEditor.Domain.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace CodeEditor.Infrastructure.DataAccess
{
    public class InMemoryCache : IInMemoryCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        private ConcurrentDictionary<string, string> _cacheStoredKeys = [];

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _memoryCacheEntryOptions = new MemoryCacheEntryOptions();

            _memoryCacheEntryOptions.RegisterPostEvictionCallback(
                    new PostEvictionDelegate(
                        (k, _, _, _) =>
                            _cacheStoredKeys.TryRemove(k.ToString()!, out _))
                    );
        }

        public T? GetEntity<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T? entity);
            return entity;
        }

        public IEnumerable<string> GetStoredKeys(Type type)
        {
            return _cacheStoredKeys
                .ToList()
                .Where((keyValue) => keyValue.Value == type.Name)
                .Select(keyValue => keyValue.Key);
        }

        public bool SetEntity<T>(string key, T entity, TimeSpan ttl)
        {
            var result = _memoryCache.Set(key, entity, ttl);
            if (result != null)
            {
                _cacheStoredKeys.TryAdd(key, typeof(T).Name);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
