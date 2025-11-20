using AutoMapper;
using BeautySalon.API.Models;
using BeautySalon.API.DTOs;

namespace BeautySalon.API.Mapping
{
    /// <summary>
    /// Профиль маппинга между сущностями и DTO
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Client mappings
            CreateMap<Client, ClientDTO>();
            CreateMap<CreateClientDTO, Client>();
            CreateMap<UpdateClientDTO, Client>();

            // Service mappings
            CreateMap<Service, ServiceDTO>();
            CreateMap<CreateServiceDTO, Service>();

            // Employee mappings
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CreateEmployeeDTO, Employee>();

            // Appointment mappings
            CreateMap<Appointment, AppointmentDTO>()
                .ForMember(dest => dest.ClientName,
                    opt => opt.MapFrom(src => $"{src.Client.LastName} {src.Client.FirstName}"))
                .ForMember(dest => dest.ServiceName,
                    opt => opt.MapFrom(src => src.Service.Name))
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => $"{src.Employee.LastName} {src.Employee.FirstName}"));

            CreateMap<CreateAppointmentDTO, Appointment>();
        }
    }
}
