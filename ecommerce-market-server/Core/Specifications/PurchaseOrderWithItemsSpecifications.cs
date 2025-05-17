using Core.Entities.PurchaseOrder;

namespace Core.Specifications
{
    /// <summary>
    /// Especificación para consultar órdenes de compra incluyendo sus ítems y tipo de envío.
    /// </summary>
    /// <remarks>
    /// Esta clase permite construir criterios de consulta para obtener órdenes de compra
    /// junto con sus ítems asociados y el tipo de envío, filtrando por correo electrónico del comprador
    /// y, opcionalmente, por identificador de la orden. Los resultados se ordenan por fecha de compra descendente.
    /// </remarks>
    public class PurchaseOrderWithItemsSpecifications : BaseSpecification<PurchaseOrder>
    {
        public PurchaseOrderWithItemsSpecifications(string email) : base(po => po.BuyerEmail == email)
        {
            AddInclude(po => po.ItemsOrder!);
            AddInclude(po => po.ShippingType!);
            AddOrderByDescending(po => po.PurchaseOrderDate);
        }

        public PurchaseOrderWithItemsSpecifications(int id, string email) : base(po => po.BuyerEmail == email && po.Id == id)
        {
            AddInclude(po => po.ItemsOrder!);
            AddInclude(po => po.ShippingType!);
            AddOrderByDescending(po => po.PurchaseOrderDate);
        }
    }
}