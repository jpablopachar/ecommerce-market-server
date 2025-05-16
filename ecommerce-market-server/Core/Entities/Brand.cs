namespace Core.Entities
{
    /// <summary>
    /// Representa una marca de productos dentro del sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase almacena la información básica de una marca, permitiendo su identificación y asociación con productos.
    /// </remarks>
    public class Brand : BaseClass
    {
        public string? Name { get; set; }
    }
}