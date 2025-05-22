using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Data
{
    /// <summary>
    /// Clase que se encarga de cargar datos iniciales en la base de datos de seguridad.
    /// </summary>
    /// <remarks>
    /// Esta clase contiene un método estático que se encarga de cargar datos de usuario y roles.
    /// </remarks>
    /// <typeparam name="T">Tipo de entidad a cargar en la base de datos.</typeparam>
    public class SecurityDbContextData
    {
        /// <summary>
        /// Carga datos iniciales de usuario y roles en la base de datos.
        /// </summary>
        /// <param name="userManager">Instancia de UserManager para gestionar usuarios.</param>
        /// <param name="roleManager">Instancia de RoleManager para gestionar roles.</param>
        /// <param name="loggerFactory">Instancia de ILoggerFactory para registrar errores.</param>
        /// <returns>Tarea que representa la operación asíncrona.</returns>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error al cargar los datos.</exception>
        /// <remarks>
        /// Este método se encarga de crear un usuario por defecto y asignarle un rol.
        /// </remarks>
        public static async Task SeedUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var usuario = new User
                    {
                        Name = "Juan Pablo",
                        LastName = "Pachar Viñan",
                        UserName = "jppachar",
                        Email = "jppachar@yopmail.com",
                        Address = new Address
                        {
                            Street = "Cdla. Zamora",
                            City = "Loja",
                            PostalCode = "110108",
                            Department = "Loja",
                        }
                    };

                    await userManager.CreateAsync(usuario, "Jppachar7*");
                }


                if (!roleManager.Roles.Any())
                {
                    var role = new IdentityRole
                    {
                        Name = "ADMIN"
                    };

                    await roleManager.CreateAsync(role);
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<SecurityDbContextData>();
                logger.LogError(ex, "Error al cargar los datos de usuario.");
            }
        }
    }
}