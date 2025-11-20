using AutoMapper;
using BeautySalon.API.DTOs;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using BeautySalon.API.Services.Interfaces;

namespace BeautySalon.API.Services
{
    /// <summary>
    /// Сервис для работы с клиентами
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса клиентов
        /// </summary>
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDTO>>(clients);
        }

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                throw new KeyNotFoundException($"Клиент с ID {id} не найден");

            return _mapper.Map<ClientDTO>(client);
        }

        /// <summary>
        /// Создать нового клиента
        /// </summary>
        public async Task<ClientDTO> CreateClientAsync(CreateClientDTO createClientDto)
        {
            var client = _mapper.Map<Client>(createClientDto);
            await _clientRepository.AddAsync(client);
            return _mapper.Map<ClientDTO>(client);
        }

        /// <summary>
        /// Обновить клиента
        /// </summary>
        public async Task UpdateClientAsync(int id, UpdateClientDTO updateClientDto)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
            if (existingClient == null)
                throw new KeyNotFoundException($"Клиент с ID {id} не найден");

            _mapper.Map(updateClientDto, existingClient);
            _clientRepository.Update(existingClient);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        public async Task DeleteClientAsync(int id)
        {
            if (!await _clientRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"Клиент с ID {id} не найден");

            await _clientRepository.RemoveByIdAsync(id);
        }

        /// <summary>
        /// Получить клиентов с записями
        /// </summary>
        public async Task<IEnumerable<ClientDTO>> GetClientsWithAppointmentsAsync()
        {
            var clients = await _clientRepository.GetClientsWithAppointmentsAsync();
            return _mapper.Map<IEnumerable<ClientDTO>>(clients);
        }
    }
}
