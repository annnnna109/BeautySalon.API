using BeautySalon.API.DTOs;
using BeautySalon.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с услугами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        /// <summary>
        /// Конструктор контроллера услуг
        /// </summary>
        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Получить все услуги
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDTO>> GetService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            return Ok(service);
        }

        /// <summary>
        /// Получить услуги по категории
        /// </summary>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServicesByCategory(string category)
        {
            var services = await _serviceService.GetServicesByCategoryAsync(category);
            return Ok(services);
        }

        /// <summary>
        /// Получить услуги с сотрудниками
        /// </summary>
        [HttpGet("with-employees")]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServicesWithEmployees()
        {
            var services = await _serviceService.GetServicesWithEmployeesAsync();
            return Ok(services);
        }

        /// <summary>
        /// Создать новую услугу
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> PostService(CreateServiceDTO createServiceDto)
        {
            var service = await _serviceService.CreateServiceAsync(createServiceDto);
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        /// <summary>
        /// Обновить данные услуги
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, UpdateServiceDTO updateServiceDto)
        {
            await _serviceService.UpdateServiceAsync(id, updateServiceDto);
            return NoContent();
        }

        /// <summary>
        /// Удалить услугу
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
    }
}