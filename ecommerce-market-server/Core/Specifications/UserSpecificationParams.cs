namespace Core.Specifications
{
    /// <summary>
    /// Representa los parámetros de filtrado, búsqueda y paginación para la consulta de usuarios en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta clase permite especificar criterios como nombre, apellido, ordenamiento, búsqueda textual y configuración de paginación
    /// al recuperar listas de usuarios desde el repositorio.
    /// </remarks>
    public class UserSpecificationParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 3;

        public string? Name { get; set; }
        public string? LastName { get; set; }
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