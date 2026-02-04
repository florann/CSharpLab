using CodeEditor.Domain.Specifications.Interfaces;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IService<T>
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> GetAsync(ISpecification<T> spec);
        Task<IEnumerable<T>?> GetAllAsync(ISpecification<T> spec);
        Task DeleteAsync(T entity);
    }
}

