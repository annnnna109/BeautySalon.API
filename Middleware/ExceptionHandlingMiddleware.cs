using System.Net;
using System.Text.Json;

namespace BeautySalon.API.Middleware
{
    /// <summary>
    /// Глобальный обработчик исключений
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Конструктор middleware
        /// </summary>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обработка запроса
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошло необработанное исключение");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обработка исключения и формирование ответа
        /// </summary>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = new
                {
                    message = "Внутренняя ошибка сервера",
                    details = exception.Message,
                    type = exception.GetType().Name,
                    timestamp = DateTime.UtcNow
                }
            };

            // Для KeyNotFoundException возвращаем 404
            if (exception is KeyNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new
                {
                    error = new
                    {
                        message = "Ресурс не найден",
                        details = exception.Message,
                        type = exception.GetType().Name,
                        timestamp = DateTime.UtcNow
                    }
                };
            }

            // Для ArgumentException возвращаем 400
            if (exception is ArgumentException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    error = new
                    {
                        message = "Неверные параметры запроса",
                        details = exception.Message,
                        type = exception.GetType().Name,
                        timestamp = DateTime.UtcNow
                    }
                };
            }

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(result);
        }
    }
}
