using Core.Entities;

namespace Core.Specifications
{
    /// <summary>
    /// Especificación utilizada para contar productos que cumplen con los criterios de búsqueda, marca y categoría especificados.
    /// </summary>
    /// <param name="productParams">
    /// Parámetros de filtrado que incluyen búsqueda por nombre, identificador de marca e identificador de categoría.
    /// </param>
    /// <remarks>
    /// Esta especificación se emplea principalmente para obtener el número total de productos que coinciden con los filtros aplicados,
    /// sin recuperar los datos completos de los productos.
    /// </remarks>
    public class ProductForCountingSpecification(ProductSpecificationParams productParams) : BaseSpecification<Product>(p =>
            (string.IsNullOrEmpty(productParams.Search) || p.Name!.Contains(productParams.Search)) &&
            (!productParams.Brand.HasValue || p.BrandId == productParams.Brand) &&
            (!productParams.Category.HasValue || p.CategoryId == productParams.Category)
        )
    { }
}