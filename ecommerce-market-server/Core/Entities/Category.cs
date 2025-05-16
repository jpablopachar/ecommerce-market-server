namespace Core.Entities
{
    /// <summary>
    /// Representa una categoría de productos dentro del sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase se utiliza para organizar y clasificar los productos en el catálogo, facilitando la navegación y búsqueda por parte de los usuarios.
    /// </remarks>
    public class Category : BaseClass
    {
        public string? Name { get; set; }
    }
}
