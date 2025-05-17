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
        /// <summary>
        /// Obtiene de forma asíncrona un producto por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del producto.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene el producto correspondiente si existe.</returns>
        Task<Product> GetProductByIdAsync(int id);

        /// <summary>
        /// Obtiene de forma asíncrona la lista completa de productos disponibles.
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de productos.</returns>
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}