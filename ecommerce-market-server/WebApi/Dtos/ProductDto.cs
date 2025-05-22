namespace WebApi.Dtos
{
    /// <summary>
    /// Representa los datos de un producto utilizados para la transferencia entre capas de la aplicación.
    /// </summary>
    /// <remarks>
    /// Este DTO se emplea para exponer información relevante de productos en las operaciones de la API,
    /// incluyendo detalles de marca, categoría y presentación para el cliente.
    /// </remarks>
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
    }
}