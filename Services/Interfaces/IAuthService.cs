using BeautySalon.API.DTOs;

namespace BeautySalon.API.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO registerDto);

        /// <summary>
        /// Вход пользователя
        /// </summary>
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginDto);

        /// <summary>
        /// Обновление токенов
        /// </summary>
        Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenDto);

        /// <summary>
        /// Выход пользователя
        /// </summary>
        Task<bool> LogoutAsync(int userId);
    }
}