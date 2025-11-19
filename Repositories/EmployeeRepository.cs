using BeautySalon.API.Data;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.API.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BeautySalonContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithServicesAsync()
        {
            return await _dbSet
                .Include(e => e.EmployeeServices)
                .ThenInclude(es => es.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAvailableEmployeesAsync(DateTime date)
        {
            // Логика для поиска доступных сотрудников на определенную дату
            return await _dbSet
                .Where(e => e.IsActive)
                .ToListAsync();
        }
    }
}