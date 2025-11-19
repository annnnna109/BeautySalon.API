using BeautySalon.API.Models;

namespace BeautySalon.API.Repositories.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetServicesWithEmployeesAsync();
        Task<IEnumerable<Service>> GetServicesByCategoryAsync(string category);
    }
}