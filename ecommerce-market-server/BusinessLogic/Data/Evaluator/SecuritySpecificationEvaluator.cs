using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data.Evaluator
{
    /// <summary>
    /// Clase que evalúa una especificación y aplica sus criterios a una consulta de tipo <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad que implementa <see cref="IdentityUser"/> y que se utilizará en la evaluación de la especificación.
    /// </typeparam>
    /// <remarks>
    /// Esta clase es responsable de aplicar los criterios de una especificación a una consulta de tipo <see cref="IQueryable{T}"/>.
    /// </remarks>
    public class SecuritySpecificationEvaluator<T> where T : IdentityUser
    {
        /// <summary>
        /// Aplica los criterios de filtrado, ordenación, paginación e inclusión de entidades relacionadas definidos en una especificación a una consulta de tipo <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <param name="inputQuery">La consulta base sobre la que se aplicarán las condiciones de la especificación.</param>
        /// <param name="spec">La especificación que contiene los criterios y configuraciones a aplicar sobre la consulta.</param>
        /// <returns>Una consulta <see cref="IQueryable{T}"/> modificada según los criterios de la especificación proporcionada.</returns>
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