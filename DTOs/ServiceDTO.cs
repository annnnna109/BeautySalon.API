namespace BeautySalon.API.DTOs
{
    /// <summary>
    /// Data Transfer Object для услуги
    /// </summary>
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Category { get; set; }
    }

    /// <summary>
    /// DTO для создания услуги
    /// </summary>
    public class CreateServiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Category { get; set; }
    }
}