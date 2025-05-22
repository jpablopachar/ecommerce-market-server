namespace WebApi.Dtos
{
    /// <summary>
    /// Representa los datos de una orden de compra realizados por un usuario en el sistema.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para transferir la información necesaria al crear o procesar una orden de compra,
    /// incluyendo el identificador del carrito, el tipo de envío y la dirección de entrega.
    /// </remarks>
    public class PurchaseOrderDto
    {
        public string? BuyCartId { get; set; }
        public int ShippingType { get; set; }
        public AddressDto? MailingAddress { get; set; }
    }
}