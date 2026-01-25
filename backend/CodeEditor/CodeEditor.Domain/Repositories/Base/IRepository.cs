using CodeEditor.Domain.Specifications.Interfaces;

namespace CodeEditor.Domain.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec);
        Task<T?> FindOneAsync(ISpecification<T> spec);
        void DeleteAsync(T entity);
        void UpdateAsync(T entity);
        void AddAsync(T entity);
        Task SaveChangesAsync();
    }
}
