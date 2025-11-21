using BeautySalon.API.DTOs;

namespace BeautySalon.API.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с услугами
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Получить все услуги
        /// </summary>
        Task<IEnumerable<ServiceDTO>> GetAllServicesAsync();

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        Task<ServiceDTO> GetServiceByIdAsync(int id);

        /// <summary>
        /// Создать новую услугу
        /// </summary>
        Task<ServiceDTO> CreateServiceAsync(CreateServiceDTO createServiceDto);

        /// <summary>
        /// Обновить услугу
        /// </summary>
        Task UpdateServiceAsync(int id, UpdateServiceDTO updateServiceDto);

        /// <summary>
        /// Удалить услугу
        /// </summary>
        Task DeleteServiceAsync(int id);

        /// <summary>
        /// Получить услуги по категории
        /// </summary>
        Task<IEnumerable<ServiceDTO>> GetServicesByCategoryAsync(string category);

        /// <summary>
        /// Получить услуги с сотрудниками
        /// </summary>
        Task<IEnumerable<ServiceDTO>> GetServicesWithEmployeesAsync();
    }
}