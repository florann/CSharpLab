using CodeEditor.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CodeEditor.Domain.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<T?> FindOneAsync(ISpecification<T> spec)
        {
            var result = await _context.Set<T>().Where(spec.Criteria).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec)
        {
            var result = await _context.Set<T>().Where(spec.Criteria).ToListAsync();
            return result;
        }

        public void DeleteAsync(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Deleted;
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
        }

        public void AddAsync(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Added;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
