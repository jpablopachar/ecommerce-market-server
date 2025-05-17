using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato para la gestión de la unidad de trabajo en el sistema, permitiendo coordinar la persistencia de cambios y el acceso a los repositorios genéricos.
    /// </summary>
    /// <remarks>
    /// Esta interfaz sigue el patrón Unit of Work, facilitando la gestión transaccional y la integración de múltiples repositorios bajo una misma operación.
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseClass;

        Task<int> Complete();
    }
}