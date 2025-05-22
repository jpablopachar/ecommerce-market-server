using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato para el acceso y gestión de productos en el repositorio de datos.
    /// </summary>
    /// <remarks>
    /// Esta interfaz proporciona métodos asíncronos para recuperar productos individuales o listados completos,
    /// permitiendo la abstracción de la fuente de datos utilizada.
    /// </remarks>
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}