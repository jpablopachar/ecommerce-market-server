namespace Core.Entities.PurchaseOrder
{
    /// <summary>
    /// Representa una orden de compra realizada por un cliente, incluyendo detalles como el correo electrónico del comprador,
    /// la fecha de la orden, la dirección de envío, el tipo de envío, los ítems solicitados y el estado de la orden.
    /// </summary>
    /// <remarks>
    /// Esta clase es fundamental para gestionar las órdenes de compra dentro del sistema, permitiendo el seguimiento de cada transacción,
    /// así como la gestión de los productos solicitados y su estado actual.
    /// </remarks>
    public class PurchaseOrder : BaseClass
    {
        public string? BuyerEmail { get; set; }
        public DateTimeOffset PurchaseOrderDate { get; set; } = DateTimeOffset.Now;
        public Address? MailingAddress { get; set; }
        public ShippingType? ShippingType { get; set; }

        public IReadOnlyList<ItemOrder>? ItemsOrder { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus? Status { get; set; } = OrderStatus.Pending;
        public string? AttemptPaymentId { get; set; }

        public PurchaseOrder() { }

        public PurchaseOrder(string? buyerEmail, Address? mailingAddress, ShippingType? shippingType, IReadOnlyList<ItemOrder>? itemsOrder, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            MailingAddress = mailingAddress;
            ShippingType = shippingType;
            ItemsOrder = itemsOrder;
            SubTotal = subTotal;
        }

        /// <summary>
        /// Calcula el monto total de la orden de compra sumando el subtotal y el costo de envío.
        /// </summary>
        /// <returns>El valor total a pagar por la orden, incluyendo el envío.</returns>
        public decimal GetTotal()
        {
            return SubTotal + ShippingType!.Price;
        }
    }
}