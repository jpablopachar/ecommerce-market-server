using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions
{
    /// <summary>
    /// Contiene métodos de extensión para la clase <see cref="UserManager{User}"/> que permiten buscar usuarios en función de su correo electrónico.
    /// </summary>
    /// <remarks>
    /// Estos métodos facilitan la búsqueda de usuarios en la base de datos, incluyendo la carga de información adicional como direcciones.
    /// </remarks>
    public static class UserManagerExtension
    {
        /// <summary>
        /// Busca y retorna un usuario junto con su dirección en función del correo electrónico extraído de los claims del usuario autenticado.
        /// </summary>
        /// <param name="input">Instancia de <see cref="UserManager{User}"/> utilizada para acceder a los usuarios.</param>
        /// <param name="user">Principal de seguridad que contiene los claims del usuario autenticado.</param>
        /// <returns>Una instancia de <see cref="User"/> con la dirección cargada si se encuentra; de lo contrario, null.</returns>
        public static async Task<User?> SearchUserWithAddressAsync(this UserManager<User> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usr = await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            return usr;
        }

        /// <summary>
        /// Busca y retorna un usuario en función del correo electrónico extraído de los claims del usuario autenticado.
        /// </summary>
        /// <param name="input">Instancia de <see cref="UserManager{User}"/> utilizada para acceder a los usuarios.</param>
        /// <param name="user">Principal de seguridad que contiene los claims del usuario autenticado.</param>
        /// <returns>Una instancia de <see cref="User"/> si se encuentra; de lo contrario, null.</returns>
        public static async Task<User?> SearchUserAsync(this UserManager<User> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usr = await input.Users.SingleOrDefaultAsync(x => x.Email == email);

            return usr;
        }
    }
}