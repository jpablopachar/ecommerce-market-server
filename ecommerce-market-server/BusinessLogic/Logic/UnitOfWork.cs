using System.Collections;
using BusinessLogic.Data;
using BusinessLogic.Logic.Repository;
using Core.Entities;
using Core.Interfaces;

namespace BusinessLogic.Logic
{
    /// <summary>
    /// Implementación de la unidad de trabajo para gestionar la persistencia de cambios y el acceso a los repositorios genéricos.
    /// </summary>
    /// <remarks>
    /// Esta clase sigue el patrón Unit of Work, facilitando la gestión transaccional y la integración de múltiples repositorios bajo una misma operación.
    /// </remarks>
    public class UnitOfWork(MarketDbContext marketDbContext) : IUnitOfWork
    {
        private Hashtable? _repositories;
        private readonly MarketDbContext _marketDbContext = marketDbContext;

        public async Task<int> Complete()
        {
            return await _marketDbContext.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseClass
        {
            _repositories ??= [];

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _marketDbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type]!;
        }

        /// <summary>
        /// Libera los recursos no administrados utilizados por la instancia de <see cref="UnitOfWork"/>.
        /// </summary>
        /// <remarks>
        /// Este método debe llamarse cuando la instancia ya no sea necesaria para liberar correctamente
        /// los recursos asociados al contexto de base de datos.
        /// </remarks>
        public void Dispose()
        {
            _marketDbContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}