using AutoMapper;
using BeautySalon.API.DTOs;
using BeautySalon.API.Models;
using BeautySalon.API.Repositories.Interfaces;
using BeautySalon.API.Services.Interfaces;

namespace BeautySalon.API.Services
{
    /// <summary>
    /// Сервис для работы с записями на прием
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса записей
        /// </summary>
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAppointmentsWithDetailsAsync();
            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }

        /// <summary>
        /// Получить запись по ID
        /// </summary>
        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                throw new KeyNotFoundException($"Запись с ID {id} не найдена");

            var appointmentWithDetails = (await _appointmentRepository
                .FindAsync(a => a.Id == id))
                .FirstOrDefault();

            return _mapper.Map<AppointmentDTO>(appointmentWithDetails);
        }

        /// <summary>
        /// Создать новую запись
        /// </summary>
        public async Task<AppointmentDTO> CreateAppointmentAsync(CreateAppointmentDTO createAppointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDto);
            await _appointmentRepository.AddAsync(appointment);

            var createdAppointment = (await _appointmentRepository
                .FindAsync(a => a.Id == appointment.Id))
                .FirstOrDefault();

            return _mapper.Map<AppointmentDTO>(createdAppointment);
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        public async Task UpdateAppointmentAsync(int id, CreateAppointmentDTO updateAppointmentDto)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment == null)
                throw new KeyNotFoundException($"Запись с ID {id} не найдена");

            _mapper.Map(updateAppointmentDto, existingAppointment);
            _appointmentRepository.Update(existingAppointment);
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        public async Task DeleteAppointmentAsync(int id)
        {
            if (!await _appointmentRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"Запись с ID {id} не найдена");

            await _appointmentRepository.RemoveByIdAsync(id);
        }

        /// <summary>
        /// Получить записи по дате
        /// </summary>
        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByDateAsync(DateTime date)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(date);
            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }

        /// <summary>
        /// Получить записи клиента
        /// </summary>
        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByClientAsync(int clientId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByClientAsync(clientId);
            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }
    }
}
