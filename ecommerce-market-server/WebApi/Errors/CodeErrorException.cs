namespace WebApi.Errors
{
    /// <summary>
    /// Representa una excepción personalizada que incluye información adicional sobre el error ocurrido en la API.
    /// </summary>
    /// <param name="statusCode">El código de estado HTTP asociado al error.</param>
    /// <param name="message">Un mensaje descriptivo del error.</param>
    /// <param name="details">Detalles adicionales que proporcionan contexto sobre el error.</param>
    /// <remarks>
    /// Esta excepción extiende <see cref="CodeErrorResponse"/> y permite adjuntar información detallada para facilitar el diagnóstico de errores en la capa de presentación.
    /// </remarks>
    public class CodeErrorException(int statusCode, string? message = null, string? details = null) : CodeErrorResponse(statusCode, message)
    {
        public string? Details { get; set; } = details;
    }
}