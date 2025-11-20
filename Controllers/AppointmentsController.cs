using BeautySalon.API.DTOs;
using BeautySalon.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            return Ok(appointment);
        }

        /// <summary>
        /// Создать новую запись
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> PostAppointment(CreateAppointmentDTO createAppointmentDto)
        {
            var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, CreateAppointmentDTO updateAppointmentDto)
        {
            await _appointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
            return NoContent();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Получить записи по дате
        /// </summary>
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentsByDate(DateTime date)
        {
            var appointments = await _appointmentService.GetAppointmentsByDateAsync(date);
            return Ok(appointments);
        }

        /// <summary>
        /// Получить записи клиента
        /// </summary>
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointmentsByClient(int clientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByClientAsync(clientId);
            return Ok(appointments);
        }
    }
}