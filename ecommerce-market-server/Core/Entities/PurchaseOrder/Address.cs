namespace Core.Entities.PurchaseOrder
{
    /// <summary>
    /// Representa una dirección física asociada a una orden de compra.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena la información necesaria para identificar la ubicación de entrega o facturación,
    /// incluyendo calle, ciudad, departamento, código postal y país.
    /// </remarks>
    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Department { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public Address() { }

        public Address(string street, string city, string department, string postalCode, string country)
        {
            Street = street;
            City = city;
            Department = department;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
