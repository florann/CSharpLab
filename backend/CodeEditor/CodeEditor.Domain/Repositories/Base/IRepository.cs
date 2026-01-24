using CodeEditor.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec);
        Task<T?> FindOneAsync(ISpecification<T> spec);
        void DeleteAsync(T entity);
        void UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
