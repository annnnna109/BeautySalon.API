using AutoMapper;
using BeautySalon.API.DTOs;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using BeautySalon.API.Services.Interfaces;

namespace BeautySalon.API.Services
{
    /// <summary>
    /// Сервис для работы с услугами
    /// </summary>
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса услуг
        /// </summary>
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все услуги
        /// </summary>
        public async Task<IEnumerable<ServiceDTO>> GetAllServicesAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDTO>>(services);
        }

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        public async Task<ServiceDTO> GetServiceByIdAsync(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
                throw new KeyNotFoundException($"Услуга с ID {id} не найдена");

            return _mapper.Map<ServiceDTO>(service);
        }

        /// <summary>
        /// Создать новую услугу
        /// </summary>
        public async Task<ServiceDTO> CreateServiceAsync(CreateServiceDTO createServiceDto)
        {
            var service = _mapper.Map<Service>(createServiceDto);
            await _serviceRepository.AddAsync(service);
            return _mapper.Map<ServiceDTO>(service);
        }

        /// <summary>
        /// Обновить услугу
        /// </summary>
        public async Task UpdateServiceAsync(int id, UpdateServiceDTO updateServiceDto)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id);
            if (existingService == null)
                throw new KeyNotFoundException($"Услуга с ID {id} не найдена");

            _mapper.Map(updateServiceDto, existingService);
            _serviceRepository.Update(existingService);
        }

        /// <summary>
        /// Удалить услугу
        /// </summary>
        public async Task DeleteServiceAsync(int id)
        {
            if (!await _serviceRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"Услуга с ID {id} не найдена");

            await _serviceRepository.RemoveByIdAsync(id);
        }

        /// <summary>
        /// Получить услуги по категории
        /// </summary>
        public async Task<IEnumerable<ServiceDTO>> GetServicesByCategoryAsync(string category)
        {
            var services = await _serviceRepository.GetServicesByCategoryAsync(category);
            return _mapper.Map<IEnumerable<ServiceDTO>>(services);
        }

        /// <summary>
        /// Получить услуги с сотрудниками
        /// </summary>
        public async Task<IEnumerable<ServiceDTO>> GetServicesWithEmployeesAsync()
        {
            var services = await _serviceRepository.GetServicesWithEmployeesAsync();
            return _mapper.Map<IEnumerable<ServiceDTO>>(services);
        }
    }
}