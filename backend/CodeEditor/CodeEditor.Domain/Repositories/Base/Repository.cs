using CodeEditor.Domain.Specifications.Interfaces;
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

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> FindOneAsync(ISpecification<T> spec)
        {
            var query = _context
                .Set<T>()
                .AsQueryable();

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.Includes != null && spec.Includes.Any())
            {
                foreach (var include in spec.Includes)
                {
                    query = query.Include(include);
                }
            }

            if (spec.IsDesc)
            {
                query = query.OrderByDescending(spec.OrderBy);
            }
            else
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.Take > 0)
            {
                query = query.Take(spec.Take);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>?> FindAllAsync(ISpecification<T> spec)
        {
            var query = _context
                .Set<T>()
                .AsQueryable();

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.Includes != null && spec.Includes.Any())
            {
                foreach (var include in spec.Includes)
                {
                    query = query.Include(include);
                }
            }

            if (spec.IsDesc)
            {
                query = query.OrderByDescending(spec.OrderBy);
            }
            else
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.Take > 0)
            {
                query = query.Take(spec.Take);
            }

            return await query.ToListAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Added;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
