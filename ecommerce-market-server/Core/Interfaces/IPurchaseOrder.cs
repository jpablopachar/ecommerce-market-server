using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public interface IPurchaseOrder
    {
        Task<PurchaseOrder> AddPurchaseOrderAsync(string buyerEmail, int shippingType, string cartId, Address address);

        Task<IReadOnlyList<PurchaseOrder>> GetPurchaseOrdersByEmailAsync(string email);

        Task<PurchaseOrder> GetPurchaseOrderByIdAsync(int id);

        Task<IReadOnlyList<ShippingType>> GetShippingType();
    }
}