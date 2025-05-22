using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato genérico para los repositorios de seguridad que gestionan entidades en el sistema.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad gestionada por el repositorio, que debe heredar de <see cref="IdentityUser"/>.
    /// </typeparam>
    /// <remarks>
    /// Esta interfaz proporciona operaciones asíncronas para consultar, agregar y actualizar entidades,
    /// así como soporte para especificaciones personalizadas.
    /// </remarks>
    public interface IGenericSecurityRepository<T> where T : IdentityUser
    {
        Task<T?> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetByIdWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

        Task<int> Add(T entity);

        Task<int> Update(T entity);
    }
}