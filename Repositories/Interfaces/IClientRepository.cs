using BeautySalon.API.Models;

namespace BeautySalon.API.Repositories.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientsWithAppointmentsAsync();
        Task<Client> GetClientByPhoneAsync(string phone);
    }
}