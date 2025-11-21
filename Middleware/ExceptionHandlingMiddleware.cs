using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace BeautySalon.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

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

            // Обработка различных типов исключений
            switch (exception)
            {
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        error = new
                        {
                            message = "Ошибка авторизации",
                            details = exception.Message,
                            type = exception.GetType().Name,
                            timestamp = DateTime.UtcNow
                        }
                    };
                    break;

                case SecurityTokenException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        error = new
                        {
                            message = "Неверный токен",
                            details = exception.Message,
                            type = exception.GetType().Name,
                            timestamp = DateTime.UtcNow
                        }
                    };
                    break;

                case KeyNotFoundException:
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
                    break;

                case ArgumentException:
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
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(result);
        }
    }
}