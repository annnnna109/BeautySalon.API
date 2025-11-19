using BeautySalon.API.Data;
using BeautySalon.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeautySalon.API.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly BeautySalonContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(BeautySalonContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task RemoveByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await GetByIdAsync(id) != null;
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}