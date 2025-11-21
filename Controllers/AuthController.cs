using BeautySalon.API.DTOs;
using BeautySalon.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.API.Controllers
{
    /// <summary>
    /// Контроллер аутентификации
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Конструктор контроллера аутентификации
        /// </summary>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="registerDto">Данные для регистрации</param>
        /// <returns>Результат регистрации с токенами</returns>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterRequestDTO registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return CreatedAtAction(nameof(Register), result);
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="loginDto">Данные для входа</param>
        /// <returns>Токены аутентификации</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginRequestDTO loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }

        /// <summary>
        /// Обновление токенов
        /// </summary>
        /// <param name="refreshTokenDto">Токены для обновления</param>
        /// <returns>Новые токены</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDTO>> Refresh(RefreshTokenRequestDTO refreshTokenDto)
        {
            var result = await _authService.RefreshTokenAsync(refreshTokenDto);
            return Ok(result);
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        /// <returns>Результат выхода</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            await _authService.LogoutAsync(userId);
            return Ok(new { message = "Выход выполнен успешно" });
        }
    }
}