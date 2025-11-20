using AutoMapper;
using BeautySalon.API.DTOs;
using BeautySalon.API.Repositories.Interfaces;
using BeautySalon.API.Services.Interfaces;

namespace BeautySalon.API.Services
{
    /// <summary>
    /// Сервис для работы с сотрудниками
    /// </summary>
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса сотрудников
        /// </summary>
        public EmployeeManagementService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        /// <summary>
        /// Получить сотрудника по ID
        /// </summary>
        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new KeyNotFoundException($"Сотрудник с ID {id} не найден");

            return _mapper.Map<EmployeeDTO>(employee);
        }

        /// <summary>
        /// Получить сотрудников с услугами
        /// </summary>
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesWithServicesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesWithServicesAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }
    }
}