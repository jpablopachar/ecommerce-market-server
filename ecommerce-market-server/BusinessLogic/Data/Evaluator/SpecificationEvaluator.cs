using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data.Evaluator
{
    /// <summary>
    /// Clase que evalúa una especificación y aplica sus criterios a una consulta de tipo <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad que se está evaluando, que debe heredar de <see cref="BaseClass"/>.</typeparam>
    /// <remarks>
    /// Esta clase es responsable de aplicar los criterios de una especificación a una consulta de tipo <see cref="IQueryable{T}"/>.
    /// </remarks>
    public class SpecificationEvaluator<T> where T : BaseClass
    {
        /// <summary>
        /// Aplica los criterios de una especificación a una consulta de tipo <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <param name="inputQuery">La consulta de entrada sobre la que se aplicarán los filtros y modificaciones.</param>
        /// <param name="spec">La especificación que define los criterios, ordenamientos, inclusiones y paginación.</param>
        /// <returns>
        /// Una nueva consulta <see cref="IQueryable{T}"/> que incorpora los filtros, ordenamientos, inclusiones y paginación definidos en la especificación.
        /// </returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {

            if (spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }

            inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));

            return inputQuery;
        }
    }
}
