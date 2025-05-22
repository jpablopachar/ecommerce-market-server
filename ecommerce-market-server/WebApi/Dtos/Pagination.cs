namespace WebApi.Dtos
{
    /// <summary>
    /// Representa una estructura de paginación genérica para resultados de consultas.
    /// </summary>
    /// <typeparam name="T">El tipo de los elementos contenidos en la página.</typeparam>
    /// <remarks>
    /// Esta clase se utiliza para encapsular los datos paginados, incluyendo la información
    /// sobre el número total de elementos, el índice de la página actual, el tamaño de página,
    /// la cantidad total de páginas y los datos correspondientes a la página solicitada.
    /// </remarks>
    public class Pagination<T> where T : class
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T>? Data { get; set; }
        public int PageCount { get; set; }
    }
}