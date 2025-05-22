using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato para los servicios responsables de la generación de tokens de autenticación.
    /// </summary>
    /// <remarks>
    /// Esta interfaz permite la creación de tokens de seguridad basados en la información del usuario y sus roles,
    /// facilitando la autenticación y autorización en el sistema.
    /// </remarks>
    public interface ITokenService
    {
        string CreateToken(User user, IList<string>? roles);
    }
}