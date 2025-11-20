namespace BeautySalon.API.DTOs
{
    /// <summary>
    /// Data Transfer Object для записи на прием
    /// </summary>
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// DTO для создания записи
    /// </summary>
    public class CreateAppointmentDTO
    {
        public DateTime AppointmentDate { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public string? Notes { get; set; }
    }
}
