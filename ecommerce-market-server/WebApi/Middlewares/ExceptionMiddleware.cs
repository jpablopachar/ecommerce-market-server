using System.Net;
using System.Text.Json;
using WebApi.Errors;

namespace WebApi.Middlewares
{
    /// <summary>
    /// Middleware para manejar excepciones no controladas en la aplicación.
    /// </summary>
    /// <remarks>
    /// Este middleware captura las excepciones que ocurren durante la ejecución de la aplicación y devuelve una respuesta JSON estandarizada al cliente.
    /// </remarks>
    /// <param name="next">El siguiente middleware en la cadena de ejecución.</param>
    /// <param name="logger">El registrador para registrar errores.</param>
    /// <param name="env">El entorno de host para determinar si la aplicación está en desarrollo o producción.</param>
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString())
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}