namespace BeautySalon.API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Specialization { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<EmployeeService> EmployeeServices { get; set; } = new List<EmployeeService>();


    }
}
