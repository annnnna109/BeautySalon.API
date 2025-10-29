using System.ComponentModel.DataAnnotations;

namespace BeautySalon.API.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationMinutes { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<EmployeeService> EmployeeServices { get; set; } = new List<EmployeeService>();
    }
}