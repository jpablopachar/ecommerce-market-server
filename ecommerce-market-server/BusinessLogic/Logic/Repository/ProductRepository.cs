using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic.Repository
{
    /// <summary>
    /// Implementación del repositorio para gestionar productos en el sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase proporciona métodos asíncronos para recuperar productos individuales o listados completos,
    /// permitiendo la abstracción de la fuente de datos utilizada.
    /// </remarks>
    public class ProductRepository(MarketDbContext marketDbContext) : IProductRepository
    {
        private readonly MarketDbContext _marketDbContext = marketDbContext;

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _marketDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _marketDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();
        }
    }
}