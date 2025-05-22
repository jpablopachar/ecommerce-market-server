using System.Reflection;
using Core.Entities;
using Core.Entities.PurchaseOrder;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data
{
    /// <summary>
    /// Representa el contexto de base de datos principal para el sistema de comercio electrónico.
    /// </summary>
    /// <remarks>
    /// Esta clase gestiona el acceso y la configuración de las entidades del dominio, incluyendo productos, categorías, marcas, pedidos de compra, ítems de pedido y tipos de envío.
    /// Utiliza Entity Framework Core para mapear las entidades a la base de datos y aplicar las configuraciones definidas en el ensamblado.
    /// </remarks>
    public class MarketDbContext(DbContextOptions<MarketDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<ItemOrder> ItemOrders { get; set; }

        public DbSet<ShippingType> ShippingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}