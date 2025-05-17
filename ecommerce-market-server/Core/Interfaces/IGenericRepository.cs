using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato genérico para los repositorios que gestionan entidades en el sistema.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad gestionada por el repositorio, que debe heredar de <see cref="BaseClass"/>.
    /// </typeparam>
    /// <remarks>
    /// Esta interfaz proporciona operaciones asíncronas y síncronas para la obtención, conteo, adición, actualización y eliminación de entidades,
    /// permitiendo la implementación de patrones de repositorio y especificación.
    /// </remarks>
    public interface IGenericRepository<T> where T : BaseClass
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

        Task<int> Add(T entity);

        Task<int> Update(T entity);

        void AddEntity(T Entity);

        void UpdateEntity(T Entity);

        void DeleteEntity(T Entity);
    }
}