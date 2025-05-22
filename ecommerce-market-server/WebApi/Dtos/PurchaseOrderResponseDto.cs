namespace WebApi.Dtos
{
    /// <summary>
    /// Representa la respuesta de una orden de compra realizada en el sistema.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para transferir la información detallada de una orden de compra,
    /// incluyendo datos del comprador, dirección de envío, tipo de envío, productos adquiridos y totales.
    /// </remarks>
    public class PurchaseOrderResponseDto
    {
        public int Id { get; set; }
        public string? BuyerEmail { get; set; }
        public DateTimeOffset? PurchaseOrderDate { get; set; }
        public AddressDto? MailingAddress { get; set; }
        public string? ShippingType { get; set; }
        public decimal ShippingTypePrice { get; set; }
        public IReadOnlyList<ItemOrderResponseDto>? ItemOrders { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string? Status { get; set; }
    }
}