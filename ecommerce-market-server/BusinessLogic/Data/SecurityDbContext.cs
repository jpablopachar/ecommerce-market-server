using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data
{
    /// <summary>
    /// Representa el contexto de base de datos para la gestión de seguridad y usuarios en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta clase extiende <see cref="IdentityDbContext{User}"/> y configura el esquema de identidad
    /// utilizando Entity Framework Core para almacenar y gestionar la información de autenticación y autorización.
    /// </remarks>
    public class SecurityDbContext(DbContextOptions<SecurityDbContext> options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}