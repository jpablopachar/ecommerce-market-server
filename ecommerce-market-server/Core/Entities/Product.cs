namespace Core.Entities
{
    /// <summary>
    /// Representa un producto disponible en el sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase contiene la información principal de un producto, incluyendo su nombre, descripción,
    /// stock disponible, precio, imagen y las relaciones con su marca y categoría.
    /// </remarks>
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
    }
}
