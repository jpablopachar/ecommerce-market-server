using System.Runtime.Serialization;

namespace Core.Entities.PurchaseOrder
{
    /// <summary>
    /// Define los posibles estados de una orden de compra en el sistema.
    /// </summary>
    public enum OrderStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pending,

        [EnumMember(Value = "Pago Recibido")]
        ReceivedPayment,

        [EnumMember(Value = "Fallo en el Pago")]
        FailedPayment,
    }
}