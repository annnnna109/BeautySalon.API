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
        /// <returns>Список услуг</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        /// <param name="id">ID услуги</param>
        /// <returns>Данные услуги</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDTO>> GetService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            return Ok(service);
        }

        /// <summary>
        /// Получить услуги по категории
        /// </summary>
        /// <param name="category">Категория услуг</param>
        /// <returns>Список услуг в категории</returns>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServicesByCategory(string category)
        {
            var services = await _serviceService.GetServicesByCategoryAsync(category);
            return Ok(services);
        }

        /// <summary>
        /// Получить услуги с сотрудниками
        /// </summary>
        /// <returns>Список услуг с сотрудниками</returns>
        [HttpGet("with-employees")]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServicesWithEmployees()
        {
            var services = await _serviceService.GetServicesWithEmployeesAsync();
            return Ok(services);
        }

        /// <summary>
        /// Создать новую услугу
        /// </summary>
        /// <param name="createServiceDto">Данные для создания услуги</param>
        /// <returns>Созданная услуга</returns>
        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> PostService(CreateServiceDTO createServiceDto)
        {
            var service = await _serviceService.CreateServiceAsync(createServiceDto);
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        /// <summary>
        /// Обновить данные услуги
        /// </summary>
        /// <param name="id">ID услуги</param>
        /// <param name="updateServiceDto">Обновленные данные услуги</param>
        /// <returns>Результат операции</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, UpdateServiceDTO updateServiceDto)
        {
            await _serviceService.UpdateServiceAsync(id, updateServiceDto);
            return NoContent();
        }

        /// <summary>
        /// Удалить услугу
        /// </summary>
        /// <param name="id">ID услуги</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
    }
}