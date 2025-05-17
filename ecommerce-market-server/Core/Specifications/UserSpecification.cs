using Core.Entities;

namespace Core.Specifications
{
    /// <summary>
    /// Especificación utilizada para filtrar, ordenar y paginar usuarios según los parámetros proporcionados.
    /// </summary>
    /// <remarks>
    /// Esta clase permite construir criterios dinámicos de búsqueda sobre la entidad <see cref="User"/>, 
    /// aplicando filtros por nombre, apellido y términos de búsqueda, así como ordenamientos y paginación.
    /// </remarks>
    /// <param name="userParams">
    /// Parámetros de filtrado, ordenamiento y paginación para la consulta de usuarios.
    /// </param>
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(UserSpecificationParams userParams) : base(x =>
            (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search)) &&
            (string.IsNullOrEmpty(userParams.Name) || x.Name!.Contains(userParams.Name)) &&
            (string.IsNullOrEmpty(userParams.LastName) || x.LastName!.Contains(userParams.LastName))
        )
        {
            ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);

            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.Name!);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name!);
                        break;
                    case "emailAsc":
                        AddOrderBy(x => x.Email!);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(x => x.Email!);
                        break;
                    default:
                        AddOrderBy(x => x.Name!);
                        break;
                }
            }
        }
    }
}