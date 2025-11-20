using BeautySalon.API.DTOs;
using BeautySalon.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с клиентами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Конструктор контроллера клиентов
        /// </summary>
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        /// <returns>Список клиентов</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <returns>Данные клиента</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            return Ok(client);
        }

        /// <summary>
        /// Создать нового клиента
        /// </summary>
        /// <param name="createClientDto">Данные для создания клиента</param>
        /// <returns>Созданный клиент</returns>
        [HttpPost]
        public async Task<ActionResult<ClientDTO>> PostClient(CreateClientDTO createClientDto)
        {
            var client = await _clientService.CreateClientAsync(createClientDto);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        /// <summary>
        /// Обновить данные клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <param name="updateClientDto">Обновленные данные клиента</param>
        /// <returns>Результат операции</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, UpdateClientDTO updateClientDto)
        {
            await _clientService.UpdateClientAsync(id, updateClientDto);
            return NoContent();
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Получить клиентов с их записями
        /// </summary>
        /// <returns>Список клиентов с записями</returns>
        [HttpGet("with-appointments")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsWithAppointments()
        {
            var clients = await _clientService.GetClientsWithAppointmentsAsync();
            return Ok(clients);
        }
    }
}