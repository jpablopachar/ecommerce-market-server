using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data.Evaluator
{
    /// <summary>
    /// Evaluador de especificaciones para entidades que heredan de <see cref="IdentityUser"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que hereda de <see cref="IdentityUser"/>.</typeparam>
    /// <remarks>
    /// Esta clase implementa <see cref="ISpecificationEvaluator{TEntity}"/> para definir la lógica de evaluación de especificaciones.
    /// </remarks>
    public class SecuritySpecificationEvaluator<T> where T : IdentityUser
    {
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