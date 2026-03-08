using CodeEditor.Domain.Entities.Base;

namespace CodeEditor.Domain.DataAccess
{
    public interface IInMemoryCache 
    {
        public bool SetEntity<T>(string key, T entity, TimeSpan ttl);
        public T? GetEntity<T>(string key);
        public IEnumerable<string> GetStoredKeys(Type type);
    }
}
