using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BeautySalon.API.Data;
using BeautySalon.API.DTOs;
using BeautySalon.API.Models;
using BeautySalon.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BeautySalon.API.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly BeautySalonContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса аутентификации
        /// </summary>
        public AuthService(BeautySalonContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO registerDto)
        {
            // Проверка существования пользователя
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                throw new ArgumentException("Пользователь с таким именем уже существует");

            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                throw new ArgumentException("Пользователь с таким email уже существует");

            // Создание пользователя
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Генерация токенов
            var tokens = GenerateTokens(user);

            return new AuthResponseDTO
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(GetTokenExpireMinutes()),
                Message = "Пользователь успешно зарегистрирован"
            };
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginDto)
        {
            // Поиск пользователя по email или username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Login || u.Username == loginDto.Login);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Неверные учетные данные");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Учетная запись заблокирована");

            // Генерация токенов
            var tokens = GenerateTokens(user);

            return new AuthResponseDTO
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(GetTokenExpireMinutes()),
                Message = "Вход выполнен успешно"
            };
        }

        /// <summary>
        /// Обновление токенов
        /// </summary>
        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken);
            var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = await _context.Users.FindAsync(userId);
            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException("Пользователь не найден или заблокирован");

            // Генерация новых токенов
            var tokens = GenerateTokens(user);

            return new AuthResponseDTO
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(GetTokenExpireMinutes()),
                Message = "Токены обновлены"
            };
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        public async Task<bool> LogoutAsync(int userId)
        {
            // В простой реализации просто возвращаем true
            // В реальном приложении здесь можно добавить логику инвалидации токенов
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Генерация JWT токена
        /// </summary>
        private (string accessToken, string refreshToken) GenerateTokens(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(GetTokenExpireMinutes()),
                signingCredentials: creds);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            return (accessToken, refreshToken);
        }

        /// <summary>
        /// Генерация refresh токена
        /// </summary>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Получение principal из истекшего токена
        /// </summary>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        /// <summary>
        /// Получение времени жизни токена из конфигурации
        /// </summary>
        private int GetTokenExpireMinutes()
        {
            return int.TryParse(_configuration["Jwt:ExpireMinutes"], out var minutes) ? minutes : 60;
        }
    }
}