namespace Core.Entities
{
    /// <summary>
    /// Representa una dirección física asociada a un usuario en el sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena la información necesaria para identificar la ubicación de un usuario,
    /// incluyendo calle, ciudad, departamento, código postal y país. Se utiliza principalmente
    /// para propósitos de envío y facturación.
    /// </remarks>
    public class Address : BaseClass
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Department { get; set; }
        public string? PostalCode { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string? Country { get; set; }
    }
}