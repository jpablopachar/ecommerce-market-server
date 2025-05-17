namespace Core.Entities.PurchaseOrder
{
    /// <summary>
    /// Representa un tipo de envío disponible para las órdenes de compra.
    /// </summary>
    /// <remarks>
    /// Esta clase contiene la información relevante sobre las opciones de envío, incluyendo el nombre, la descripción, el tiempo estimado de entrega y el precio asociado.
    /// </remarks>
    public class ShippingType : BaseClass
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}