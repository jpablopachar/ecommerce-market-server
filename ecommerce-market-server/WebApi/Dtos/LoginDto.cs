namespace WebApi.Dtos
{
    /// <summary>
    /// Clase que representa un objeto de transferencia de datos (DTO) para el inicio de sesión de un usuario.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para transferir información sobre las credenciales de inicio de sesión de un usuario.
    /// </remarks>
    public class LoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}