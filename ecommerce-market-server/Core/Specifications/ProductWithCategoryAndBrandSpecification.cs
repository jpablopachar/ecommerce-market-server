using Core.Entities;

namespace Core.Specifications
{
    /// <summary>
    /// Especificación que permite obtener productos incluyendo sus categorías y marcas asociadas,
    /// aplicando filtros, ordenamientos y paginación según los parámetros proporcionados.
    /// </summary>
    /// <remarks>
    /// Esta clase extiende <see cref="BaseSpecification{Product}"/> para facilitar la consulta avanzada
    /// de productos, permitiendo filtrar por búsqueda de nombre, marca, categoría, así como ordenar y paginar los resultados.
    /// Incluye automáticamente las entidades relacionadas de categoría y marca.
    /// </remarks>
    public class ProductWithCategoryAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithCategoryAndBrandSpecification(ProductSpecificationParams productParams) : base(p =>
            (string.IsNullOrEmpty(productParams.Search) || p.Name!.Contains(productParams.Search)) &&
            (!productParams.Brand.HasValue || p.BrandId == productParams.Brand) &&
            (!productParams.Category.HasValue || p.CategoryId == productParams.Category)
        )
        {
            AddInclude(p => p.Category!);
            AddInclude(p => p.Brand!);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name!);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name!);
                        break;
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "descriptionAsc":
                        AddOrderBy(p => p.Description!);
                        break;
                    case "descriptionDesc":
                        AddOrderByDescending(p => p.Description!);
                        break;
                    default:
                        AddOrderBy(p => p.Name!);
                        break;

                }
            }
        }

        public ProductWithCategoryAndBrandSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Category!);
            AddInclude(p => p.Brand!);
        }
    }
}