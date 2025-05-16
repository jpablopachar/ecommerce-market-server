using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    /// <summary>
    /// Representa un usuario del sistema con información de identidad y datos personales adicionales.
    /// </summary>
    /// <remarks>
    /// Esta clase extiende <see cref="IdentityUser"/> para incluir detalles personales y de dirección
    /// relevantes en el contexto del comercio electrónico.
    /// </remarks>
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public Address? Address { get; set; }
        public string? Image { get; set; }
    }
}