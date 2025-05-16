namespace Core.Entities
{
    /// <summary>
    /// Representa el carrito de compras de un usuario en el sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena los artículos seleccionados por el usuario antes de finalizar la compra.
    /// </remarks>
    public class BuyCart
    {
        public string? Id { get; set; }
        public List<ItemCart>? Items { get; set; } = new List<ItemCart>();

        public BuyCart() { }

        public BuyCart(string? id)
        {
            Id = id;
        }
    }
}