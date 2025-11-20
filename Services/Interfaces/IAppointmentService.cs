using BeautySalon.API.DTOs;

namespace BeautySalon.API.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с записями на прием
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync();

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        Task<AppointmentDTO> GetAppointmentByIdAsync(int id);

        /// <summary>
        /// Создать новую запись
        /// </summary>
        Task<AppointmentDTO> CreateAppointmentAsync(CreateAppointmentDTO createAppointmentDto);

        /// <summary>
        /// Обновить запись
        /// </summary>
        Task UpdateAppointmentAsync(int id, CreateAppointmentDTO updateAppointmentDto);

        /// <summary>
        /// Удалить запись
        /// </summary>
        Task DeleteAppointmentAsync(int id);

        /// <summary>
        /// Получить записи по дате
        /// </summary>
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByDateAsync(DateTime date);

        /// <summary>
        /// Получить записи клиента
        /// </summary>
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsByClientAsync(int clientId);
    }
}
