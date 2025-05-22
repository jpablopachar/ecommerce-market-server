namespace WebApi.Dtos
{
    /// <summary>
    /// Representa una dirección física asociada a un usuario en el sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena la información necesaria para identificar la ubicación de un usuario, incluyendo detalles como la calle, ciudad, departamento, código postal y país.
    /// </remarks>
    public class AddressDto
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Department { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}