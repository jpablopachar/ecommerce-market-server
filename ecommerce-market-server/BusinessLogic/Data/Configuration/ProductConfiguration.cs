using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessLogic.Data.Configuration
{
    /// <summary>
    /// Configuración de la entidad <see cref="Product"/> para el contexto de datos.
    /// </summary>
    /// <remarks>
    /// Esta clase implementa <see cref="IEntityTypeConfiguration{TEntity}"/> para definir la configuración de la entidad <see cref="Product"/>.
    /// </remarks>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Image).HasMaxLength(1000);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(m => m.Brand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(c => c.Category).WithMany().HasForeignKey(p => p.CategoryId);
        }
    }
}