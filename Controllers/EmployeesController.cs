using BeautySalon.API.DTOs;
using BeautySalon.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с сотрудниками
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeManagementService _employeeService;

        /// <summary>
        /// Конструктор контроллера сотрудников
        /// </summary>
        public EmployeesController(IEmployeeManagementService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Получить сотрудника по ID
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns>Данные сотрудника</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        /// <summary>
        /// Получить сотрудников с их услугами
        /// </summary>
        /// <returns>Список сотрудников с услугами</returns>
        [HttpGet("with-services")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesWithServices()
        {
            var employees = await _employeeService.GetEmployeesWithServicesAsync();
            return Ok(employees);
        }
    }
}