using System.Text.Json;
using Core.Entities;
using Core.Entities.PurchaseOrder;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Data
{
    /// <summary>
    /// Clase que se encarga de cargar datos iniciales en la base de datos del sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase contiene un método estático que se encarga de cargar datos de marcas, categorías, productos y tipos de envío desde archivos JSON.
    /// Si la base de datos ya contiene datos, no se volverán a cargar.
    /// </remarks>
    /// <typeparam name="T">Tipo de entidad a cargar en la base de datos.</typeparam>
    public class MarketDbContextData
    {
        public static async Task LoadDataAsync(MarketDbContext marketDbContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!marketDbContext.Brands.Any())
                {
                    var brandsData = File.ReadAllText("../BusinessLogic/LoadData/brand.json");
                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);

                    foreach (var brand in brands!)
                    {
                        await marketDbContext.Brands.AddAsync(brand);
                    }

                    await marketDbContext.SaveChangesAsync();
                }

                if (!marketDbContext.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../BusinessLogic/LoadData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var category in categories!)
                    {
                        await marketDbContext.Categories.AddAsync(category);
                    }

                    await marketDbContext.SaveChangesAsync();
                }

                if (!marketDbContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../BusinessLogic/LoadData/product.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products!)
                    {
                        await marketDbContext.Products.AddAsync(product);
                    }

                    await marketDbContext.SaveChangesAsync();
                }

                if (!marketDbContext.ShippingTypes.Any())
                {
                    var shippingTypesData = File.ReadAllText("../BusinessLogic/LoadData/shippingType.json");
                    var shippingTypes = JsonSerializer.Deserialize<List<ShippingType>>(shippingTypesData);

                    foreach (var shippingType in shippingTypes!)
                    {
                        await marketDbContext.ShippingTypes.AddAsync(shippingType);
                    }

                    await marketDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();

                logger.LogError(ex, "Ha ocurrido un error al cargar los datos iniciales en la base de datos.");
            }
        }
    }
}