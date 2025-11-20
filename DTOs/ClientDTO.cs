namespace BeautySalon.API.DTOs
{
    /// <summary>
    /// Data Transfer Object для клиента
    /// </summary>
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
    }

    /// <summary>
    /// DTO для создания нового клиента
    /// </summary>
    public class CreateClientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
    }

    /// <summary>
    /// DTO для обновления клиента
    /// </summary>
    public class UpdateClientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
    }
}