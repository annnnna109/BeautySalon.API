using System.Linq.Expressions;

namespace BeautySalon.API.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // CRUD операции
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task RemoveByIdAsync(int id);

        Task<bool> ExistsAsync(int id);
        Task<int> CountAsync();
    }
}