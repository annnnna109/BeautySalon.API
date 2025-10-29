using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.API.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Запланирован";

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
        public Client Client { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}