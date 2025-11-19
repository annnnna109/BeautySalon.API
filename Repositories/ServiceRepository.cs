using BeautySalon.API.Data;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.API.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(BeautySalonContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Service>> GetServicesWithEmployeesAsync()
        {
            return await _dbSet
                .Include(s => s.EmployeeServices)
                .ThenInclude(es => es.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(s => s.Category == category)
                .ToListAsync();
        }
    }
}