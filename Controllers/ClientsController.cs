using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _clientRepository.GetAllAsync();
            return Ok(clients);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _clientRepository.Update(client);

            return NoContent();
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            await _clientRepository.AddAsync(client);
            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientRepository.RemoveByIdAsync(id);
            return NoContent();
        }

        // GET: api/Clients/with-appointments
        [HttpGet("with-appointments")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsWithAppointments()
        {
            var clients = await _clientRepository.GetClientsWithAppointmentsAsync();
            return Ok(clients);
        }
    }
}