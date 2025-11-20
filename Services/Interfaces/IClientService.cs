using BeautySalon.API.DTOs;


namespace BeautySalon.API.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с клиентами
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        Task<IEnumerable<ClientDTO>> GetAllClientsAsync();

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        Task<ClientDTO> GetClientByIdAsync(int id);

        /// <summary>
        /// Создать нового клиента
        /// </summary>
        Task<ClientDTO> CreateClientAsync(CreateClientDTO createClientDto);

        /// <summary>
        /// Обновить клиента
        /// </summary>
        Task UpdateClientAsync(int id, UpdateClientDTO updateClientDto);

        /// <summary>
        /// Удалить клиента
        /// </summary>
        Task DeleteClientAsync(int id);

        /// <summary>
        /// Получить клиентов с записями
        /// </summary>
        Task<IEnumerable<ClientDTO>> GetClientsWithAppointmentsAsync();
    }
}