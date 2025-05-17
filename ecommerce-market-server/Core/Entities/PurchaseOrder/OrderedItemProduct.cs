namespace Core.Entities.PurchaseOrder
{
    /// <summary>
    /// Representa un producto individual incluido en un pedido.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena la información básica de un producto tal como fue solicitado en una orden de compra,
    /// incluyendo su identificador, nombre y la URL de su imagen asociada.
    /// </remarks>
    public class OrderedItemProduct
    {
        public int ItemProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ImageUrl { get; set; }

        public OrderedItemProduct() { }

        public OrderedItemProduct(int itemProductId, string? productName, string? imageUrl)
        {
            ItemProductId = itemProductId;
            ProductName = productName;
            ImageUrl = imageUrl;
        }
    }
}