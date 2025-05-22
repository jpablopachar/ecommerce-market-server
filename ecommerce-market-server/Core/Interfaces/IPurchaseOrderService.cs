using Core.Entities.PurchaseOrder;

namespace Core.Interfaces
{
    /// <summary>
    /// Define el contrato para la gestión de órdenes de compra en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta interfaz establece las operaciones principales para crear y consultar órdenes de compra,
    /// así como para obtener los tipos de envío disponibles.
    /// </remarks>
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrder?> AddPurchaseOrderAsync(string buyerEmail, int shippingType, string cartId, Address address);

        Task<IReadOnlyList<PurchaseOrder>> GetPurchaseOrdersByEmailAsync(string email);

        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id, string email);

        Task<IReadOnlyList<ShippingType>> GetShippingType();
    }
}