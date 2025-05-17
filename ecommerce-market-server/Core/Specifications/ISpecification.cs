using System.Linq.Expressions;

namespace Core.Specifications
{
    /// <summary>
    /// Define el contrato para especificaciones que encapsulan criterios de consulta, inclusión de entidades relacionadas y opciones de paginación y ordenamiento.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad sobre el cual se aplica la especificación.</typeparam>
    /// <remarks>
    /// Esta interfaz permite construir consultas reutilizables y flexibles, facilitando la separación de lógica de filtrado, ordenamiento e inclusión de datos relacionados en el acceso a datos.
    /// </remarks>
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}