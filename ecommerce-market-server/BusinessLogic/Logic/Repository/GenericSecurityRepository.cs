using BusinessLogic.Data;
using BusinessLogic.Data.Evaluator;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic.Repository
{
    /// <summary>
    /// Implementación genérica del repositorio para gestionar entidades de seguridad en el sistema.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad gestionada por el repositorio, que debe heredar de <see cref="IdentityUser"/>.
    /// </typeparam>
    /// <remarks>
    /// Esta clase proporciona operaciones asíncronas para consultar, agregar y actualizar entidades,
    /// así como soporte para especificaciones personalizadas.
    /// </remarks>
    public class GenericSecurityRepository<T>(SecurityDbContext securityDbContext) : IGenericSecurityRepository<T> where T : IdentityUser
    {
        private readonly SecurityDbContext _securityDbContext = securityDbContext;

        public async Task<int> Add(T entity)
        {
            await _securityDbContext.Set<T>().AddAsync(entity);

            return await _securityDbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _securityDbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _securityDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> Update(T entity)
        {
            _securityDbContext.Set<T>().Attach(entity);

            _securityDbContext.Entry(entity).State = EntityState.Modified;

            return await _securityDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Aplica una especificación al conjunto de entidades del tipo <typeparamref name="T"/> para filtrar y construir la consulta correspondiente.
        /// </summary>
        /// <param name="spec">La especificación que define los criterios de filtrado y proyección a aplicar sobre la consulta.</param>
        /// <returns>Una consulta <see cref="IQueryable{T}"/> que representa el conjunto de entidades filtradas según la especificación proporcionada.</returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SecuritySpecificationEvaluator<T>.GetQuery(_securityDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}