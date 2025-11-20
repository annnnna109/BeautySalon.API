namespace BeautySalon.API.DTOs
{
    /// <summary>
    /// Data Transfer Object для сотрудника
    /// </summary>
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Position { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// DTO для создания сотрудника
    /// </summary>
    public class CreateEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Position { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}