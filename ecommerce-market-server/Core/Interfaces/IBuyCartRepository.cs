using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato para las operaciones de acceso y manipulación de carritos de compra en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta interfaz proporciona métodos asíncronos para obtener, actualizar y eliminar carritos de compra,
    /// permitiendo la gestión eficiente de los carritos asociados a los usuarios.
    /// </remarks>
    public interface IBuyCartRepository
    {
        Task<BuyCart?> GetBuyCartByIdAsync(string cartId);

        Task<BuyCart?> UpdateBuyCartAsync(BuyCart buyCart);

        Task<bool> DeleteBuyCartAsync(string cartId);
    }
}