using BeautySalon.API.Models;

namespace BeautySalon.API.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesWithServicesAsync();
        Task<IEnumerable<Employee>> GetAvailableEmployeesAsync(DateTime date);
    }
}