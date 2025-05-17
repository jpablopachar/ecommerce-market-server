/// <summary>
/// Representa un ítem dentro de una orden de compra, incluyendo el producto, precio y cantidad solicitada.
/// </summary>
/// <remarks>
/// Esta clase se utiliza para detallar cada producto individual en una orden de compra, permitiendo el seguimiento de los productos adquiridos, su precio y la cantidad correspondiente.
/// </remarks>
namespace Core.Entities.PurchaseOrder
{
    public class ItemOrder : BaseClass
    {
        public OrderedItemProduct? OrderedItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ItemOrder() { }

        public ItemOrder(OrderedItemProduct? orderedItem, decimal price, int quantity)
        {
            OrderedItem = orderedItem;
            Price = price;
            Quantity = quantity;
        }
    }
}