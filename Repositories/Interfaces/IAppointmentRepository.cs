using BeautySalon.API.Models;

namespace BeautySalon.API.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsWithDetailsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<IEnumerable<Appointment>> GetAppointmentsByClientAsync(int clientId);
    }
}