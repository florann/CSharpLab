using CodeEditor.Domain.Specifications.Interfaces;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IService<T>
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> FindOneAsync(ISpecification<T> spec);
        Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<bool> DeleteAsync(T entity);
    }
}

