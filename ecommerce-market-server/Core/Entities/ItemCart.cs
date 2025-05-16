namespace Core.Entities
{
    /// <summary>
    /// Representa un elemento dentro del carrito de compras del usuario.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena la información esencial de un producto añadido al carrito, incluyendo detalles como precio, cantidad y atributos descriptivos.
    /// </remarks>
    public class ItemCart
    {
        public int Id { get; set; }
        public string? Product { get; set; }
        public decimal Price { get; set; }
        public int Cant { get; set; }
        public string? Image { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}