using BeautySalon.API.DTOs;
using BeautySalon.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с записями на прием
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Конструктор контроллера записей
        /// </summary>
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список записей</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Данные записи</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            return Ok(appointment);
        }

        /// <summary>
        /// Создать новую запись
        /// </summary>
        /// <param name="createAppointmentDto">Данные для создания записи</param>
        /// <returns>Созданная запись</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<AppointmentDTO>> PostAppointment(CreateAppointmentDTO createAppointmentDto)
        {
            var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <param name="updateAppointmentDto">Обновленные данные записи</param>
        /// <returns>Результат операции</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, CreateAppointmentDTO updateAppointmentDto)
        {
            await _appointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
            return NoContent();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Получить записи по дате
        /// </summary>
        /// <param name="date">Дата для фильтрации</param>
        /// <returns>Список записей на указанную дату</returns>
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentsByDate(DateTime date)
        {
            var appointments = await _appointmentService.GetAppointmentsByDateAsync(date);
            return Ok(appointments);
        }

        /// <summary>
        /// Получить записи клиента
        /// </summary>
        /// <param name="clientId">ID клиента</param>
        /// <returns>Список записей клиента</returns>
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentsByClient(int clientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByClientAsync(clientId);
            return Ok(appointments);
        }
    }
}