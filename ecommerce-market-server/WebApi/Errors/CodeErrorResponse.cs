namespace WebApi.Errors
{
    /// <summary>
    /// Representa una respuesta de error estándar utilizada para comunicar códigos de estado y mensajes de error en la API.
    /// </summary>
    /// <remarks>
    /// Esta clase se utiliza para estructurar las respuestas de error enviadas al cliente, proporcionando tanto el código de estado HTTP como un mensaje descriptivo.
    /// </remarks>
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private static string? GetDefaultMessageStatusCode(int statusCode) {
            return statusCode switch
            {
                400 => "El Request enviado tiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "No se encontró el item buscado",
                500 => "Se producieron errores en el servidor",
                _ => null
            };
        }
    }
}