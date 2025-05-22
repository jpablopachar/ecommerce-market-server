namespace WebApi.Dtos
{
    /// <summary>
    /// Representa los datos de usuario transferidos entre la API y los clientes.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para exponer la información básica y de autenticación de un usuario
    /// en el sistema de comercio electrónico.
    /// </remarks>
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Token { get; set; }
        public bool Admin { get; set; }
    }
}