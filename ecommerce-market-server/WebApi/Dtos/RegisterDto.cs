namespace WebApi.Dtos
{
    /// <summary>
    /// Representa los datos requeridos para registrar un nuevo usuario en el sistema.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para transferir la información necesaria durante el proceso de registro de usuarios,
    /// incluyendo credenciales y datos personales básicos.
    /// </remarks>
    public class RegisterDto
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
    }
}