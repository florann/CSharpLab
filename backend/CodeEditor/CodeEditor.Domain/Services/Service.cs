using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class Service<T> : IService<T> where T : class
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            _repository.Add(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _repository.Delete(entity);
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec)
        {
            return await _repository.FindAllAsync(spec);
        }

        public async Task<T?> FindOneAsync(ISpecification<T> spec)
        {
            return await _repository.FindOneAsync(spec);
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }
    }
}
