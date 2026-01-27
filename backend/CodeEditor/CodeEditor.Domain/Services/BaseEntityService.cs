using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.Interfaces;

namespace CodeEditor.Domain.Services
{
    public class BaseEntityService<T> : IBaseEntityService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseEntityService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(ISpecification<T> spec)
        {
            var result = await _repository.FindAllAsync(spec);
            return result;
        }

        public async Task<T?> GetAsync(ISpecification<T> spec)
        {
            var result = await _repository.FindOneAsync(spec);
            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }
    }
}
