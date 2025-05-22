namespace WebApi.Dtos
{
    /// <summary>
    /// Representa la información de un producto incluido en una orden para la respuesta de la API.
    /// </summary>
    /// <remarks>
    /// Este DTO se utiliza para transferir los datos de cada producto asociado a una orden,
    /// incluyendo identificador, nombre, imagen, precio y cantidad solicitada.
    /// </remarks>
    public class ItemOrderResponseDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}