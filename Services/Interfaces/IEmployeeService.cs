using BeautySalon.API.DTOs;

namespace BeautySalon.API.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с сотрудниками
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();

        /// <summary>
        /// Получить сотрудника по ID
        /// </summary>
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);

        /// <summary>
        /// Получить сотрудников с услугами
        /// </summary>
        Task<IEnumerable<EmployeeDTO>> GetEmployeesWithServicesAsync();
    }
}