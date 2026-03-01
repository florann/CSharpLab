using CodeEditor.Domain.Specifications;

namespace CodeEditor.Domain.DataAccess
{
    public interface IMultiLayerDataAccessService<T> where T : class
    {
        Task<List<T>?> GetMultipleEntityValue(string entityName, List<long> ids, Specification<T> spec);
        Task<T?> GetEntityValue(string entityName, long id, Specification<T> spec);
        Task<bool> SetEntityValueInMemory(string entityName, long id, T entity);
        Task<bool> SetEntityValueRedis(string entityName, long id, T entity);
    }
}
