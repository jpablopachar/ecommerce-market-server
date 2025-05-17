namespace Core.Specifications
{
    /// <summary>
    /// Representa los parámetros de filtrado, búsqueda, ordenación y paginación para la consulta de productos.
    /// </summary>
    /// <remarks>
    /// Esta clase permite especificar criterios como marca, categoría, ordenamiento, búsqueda textual y configuración de paginación
    /// al recuperar listas de productos desde el repositorio.
    /// </remarks>
    public class ProductSpecificationParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 3;

        public int? Brand { get; set; }

        public int? Category { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        public string? Search { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}