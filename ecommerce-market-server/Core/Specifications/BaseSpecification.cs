using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications
{
    /// <summary>
    /// Proporciona una implementación base para especificaciones utilizadas en consultas de repositorios.
    /// </summary>
    /// <remarks>
    /// Esta clase permite definir criterios de filtrado, inclusión de entidades relacionadas,
    /// ordenamiento y paginación de resultados de manera flexible y reutilizable.
    /// Se utiliza como clase base para crear especificaciones personalizadas.
    /// </remarks>
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            Criteria = _ => true;
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = [];

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}