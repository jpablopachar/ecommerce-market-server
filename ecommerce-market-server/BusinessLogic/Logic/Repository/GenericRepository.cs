using BusinessLogic.Data;
using BusinessLogic.Data.Evaluator;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic.Repository
{
    /// <summary>
    /// Implementación genérica del repositorio para gestionar entidades en el sistema de comercio electrónico.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad gestionada por el repositorio, que debe heredar de <see cref="BaseClass"/>.
    /// </typeparam>
    /// <remarks>
    /// Esta clase proporciona operaciones asíncronas y síncronas para la obtención, conteo, adición, actualización y eliminación de entidades,
    /// permitiendo la implementación de patrones de repositorio y especificación.
    /// </remarks>
    public class GenericRepository<T>(MarketDbContext marketDbContext) : IGenericRepository<T> where T : BaseClass
    {
        private readonly MarketDbContext _marketDbContext = marketDbContext;

        public async Task<int> Add(T entity)
        {
            await _marketDbContext.Set<T>().AddAsync(entity);

            return await _marketDbContext.SaveChangesAsync();
        }

        public void AddEntity(T Entity)
        {
            _marketDbContext.Set<T>().Add(Entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void DeleteEntity(T Entity)
        {
            _marketDbContext.Set<T>().Remove(Entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _marketDbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _marketDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> Update(T entity)
        {
            _marketDbContext.Set<T>().Attach(entity);

            _marketDbContext.Entry(entity).State = EntityState.Modified;

            return await _marketDbContext.SaveChangesAsync();
        }

        public void UpdateEntity(T Entity)
        {
            _marketDbContext.Set<T>().Attach(Entity);

            _marketDbContext.Entry(Entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Aplica una especificación al conjunto de entidades del tipo <typeparamref name="T"/> para construir una consulta filtrada.
        /// </summary>
        /// <param name="spec">La especificación que define los criterios de filtrado y proyección.</param>
        /// <returns>Una consulta <see cref="IQueryable{T}"/> que incorpora los criterios definidos en la especificación.</returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_marketDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}