namespace WebApi.Dtos
{
    /// <summary>
    /// Clase que representa un objeto de transferencia de datos (DTO) para roles en la aplicación.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para transferir información sobre roles, incluyendo su nombre y estado (activo o inactivo).
    /// </remarks>
    public class RoleDto
    {
        public string? Name { get; set; }
        public bool Status { get; set; }
    }
}