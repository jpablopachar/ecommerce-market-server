using Core.Entities;

namespace Core.Specifications
{
    /// <summary>
    /// Especificación utilizada para contar usuarios que cumplen con los criterios de búsqueda proporcionados.
    /// </summary>
    /// <param name="userParams">Parámetros de filtrado que incluyen criterios como búsqueda general, nombre y apellido.</param>
    /// <remarks>
    /// Esta especificación permite filtrar usuarios por nombre, apellido o un término de búsqueda general,
    /// facilitando operaciones de conteo en consultas personalizadas.
    /// </remarks>
    public class UserForCountingSpecification(UserSpecificationParams userParams) : BaseSpecification<User>(u =>
            (string.IsNullOrEmpty(userParams.Search) || u.Name!.Contains(userParams.Search)) &&
            (string.IsNullOrEmpty(userParams.Name) || u.Name!.Contains(userParams.Name)) &&
            (string.IsNullOrEmpty(userParams.LastName) || u.LastName!.Contains(userParams.LastName))
        )
    {
    }
}