using System.ComponentModel.DataAnnotations;

namespace BeautySalon.API.DTOs
{
    /// <summary>
    /// DTO для запроса регистрации
    /// </summary>
    public class RegisterRequestDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "Client";
    }

    /// <summary>
    /// DTO для запроса входа
    /// </summary>
    public class LoginRequestDTO
    {
        [Required]
        public string Login { get; set; } // Может быть email или username

        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// DTO для ответа аутентификации
    /// </summary>
    public class AuthResponseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// DTO для обновления токена
    /// </summary>
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}