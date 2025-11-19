using BeautySalon.API.Data;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.API.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(BeautySalonContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Client>> GetClientsWithAppointmentsAsync()
        {
            return await _dbSet
                .Include(c => c.Appointments)
                .ThenInclude(a => a.Service)
                .ToListAsync();
        }

        public async Task<Client> GetClientByPhoneAsync(string phone)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Phone == phone);
        }
    }
}