using CodeEditor.Domain.Specifications.Interfaces;

namespace CodeEditor.Domain.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec);
        Task<T?> FindOneAsync(ISpecification<T> spec);
        void Delete(T entity);
        void Update(T entity);
        void Add(T entity);
        Task<int> SaveChangesAsync();
    }
}
