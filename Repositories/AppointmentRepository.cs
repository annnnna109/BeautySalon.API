using BeautySalon.API.Data;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.API.Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(BeautySalonContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsWithDetailsAsync()
        {
            return await _dbSet
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Include(a => a.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _dbSet
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Where(a => a.AppointmentDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByClientAsync(int clientId)
        {
            return await _dbSet
                .Include(a => a.Service)
                .Include(a => a.Employee)
                .Where(a => a.ClientId == clientId)
                .ToListAsync();
        }
    }
}