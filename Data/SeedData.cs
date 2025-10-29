using Microsoft.EntityFrameworkCore;
using BeautySalon.API.Models;

namespace BeautySalon.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BeautySalonContext(
                serviceProvider.GetRequiredService<DbContextOptions<BeautySalonContext>>()))
            {
                if (context.Clients.Any() || context.Services.Any() || context.Employees.Any())
                {
                    return; 
                }
                var clients = new List<Client>
                {
                    new Client { FirstName = "Анна", LastName = "Иванова", Phone = "+79161234567", Email = "anna@mail.ru" },
                    new Client { FirstName = "Мария", LastName = "Петрова", Phone = "+79161234568", Email = "maria@mail.ru" },
                    new Client { FirstName = "Елена", LastName = "Сидорова", Phone = "+79161234569", Email = "elena@mail.ru" },
                    new Client { FirstName = "Ольга", LastName = "Кузнецова", Phone = "+79161234570", Email = "olga@mail.ru" },
                    new Client { FirstName = "Ирина", LastName = "Смирнова", Phone = "+79161234571", Email = "irina@mail.ru" }
                };
                context.Clients.AddRange(clients);
                context.SaveChanges();
                var clientIds = clients.Select(c => c.Id).ToList();

                var services = new List<Service>
                {
                    new Service { Name = "Стрижка женская", Description = "Модная стрижка с укладкой", Price = 1500, DurationMinutes = 60 },
                    new Service { Name = "Окрашивание волос", Description = "Стойкое окрашивание", Price = 3000, DurationMinutes = 120 },
                    new Service { Name = "Маникюр", Description = "Аппаратный маникюр с покрытием", Price = 1200, DurationMinutes = 90 },
                    new Service { Name = "Педикюр", Description = "Комбинированный педикюр", Price = 1500, DurationMinutes = 90 },
                    new Service { Name = "Макияж", Description = "Вечерний макияж", Price = 2000, DurationMinutes = 60 }
                };
                context.Services.AddRange(services);
                context.SaveChanges();

                var serviceIds = services.Select(s => s.Id).ToList();

                var employees = new List<Employee>
                {
                    new Employee { FirstName = "Светлана", LastName = "Волкова", Specialization = "Парикмахер", Phone = "+79161234572", Email = "svetlana@salon.ru" },
                    new Employee { FirstName = "Наталья", LastName = "Орлова", Specialization = "Колорист", Phone = "+79161234573", Email = "natalia@salon.ru" },
                    new Employee { FirstName = "Татьяна", LastName = "Лебедева", Specialization = "Мастер маникюра", Phone = "+79161234574", Email = "tatiana@salon.ru" },
                    new Employee { FirstName = "Юлия", LastName = "Новикова", Specialization = "Визажист", Phone = "+79161234575", Email = "julia@salon.ru" },
                    new Employee { FirstName = "Александра", LastName = "Морозова", Specialization = "Бровист", Phone = "+79161234576", Email = "alexandra@salon.ru" }
                };
                context.Employees.AddRange(employees);
                context.SaveChanges();

                var employeeIds = employees.Select(e => e.Id).ToList();

                var employeeServices = new List<EmployeeService>
                {
                    new EmployeeService { EmployeeId = employeeIds[0], ServiceId = serviceIds[0] },
                    new EmployeeService { EmployeeId = employeeIds[0], ServiceId = serviceIds[1] },
                    new EmployeeService { EmployeeId = employeeIds[1], ServiceId = serviceIds[1] },
                    new EmployeeService { EmployeeId = employeeIds[2], ServiceId = serviceIds[2] },
                    new EmployeeService { EmployeeId = employeeIds[2], ServiceId = serviceIds[3] },
                    new EmployeeService { EmployeeId = employeeIds[3], ServiceId = serviceIds[4] },
                    new EmployeeService { EmployeeId = employeeIds[4], ServiceId = serviceIds[2] }
                };
                context.EmployeeServices.AddRange(employeeServices);
                context.SaveChanges();

                var appointments = new List<Appointment>
                {
                    new Appointment
                    {
                        ClientId = clientIds[0],
                        EmployeeId = employeeIds[0],
                        ServiceId = serviceIds[0],
                        AppointmentDate = DateTime.Today.AddDays(1),
                        StartTime = DateTime.Today.AddDays(1).AddHours(10),
                        EndTime = DateTime.Today.AddDays(1).AddHours(11),
                        Status = "Запланирован",
                        Notes = "Предпочтительна стрижка каре"
                    },
                    new Appointment
                    {
                        ClientId = clientIds[1],
                        EmployeeId = employeeIds[1],
                        ServiceId = serviceIds[1],
                        AppointmentDate = DateTime.Today.AddDays(1),
                        StartTime = DateTime.Today.AddDays(1).AddHours(14),
                        EndTime = DateTime.Today.AddDays(1).AddHours(16),
                        Status = "Запланирован"
                    },
                };
                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}