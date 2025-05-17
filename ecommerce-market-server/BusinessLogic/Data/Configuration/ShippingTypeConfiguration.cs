using Core.Entities.PurchaseOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessLogic.Data.Configuration
{
    /// <summary>
    /// Configuración de la entidad <see cref="ShippingType"/> para el contexto de datos.
    /// </summary>
    /// <remarks>
    /// Esta clase implementa <see cref="IEntityTypeConfiguration{TEntity}"/> para definir la configuración de la entidad <see cref="ShippingType"/>.
    /// </remarks>
    public class ShippingTypeConfiguration : IEntityTypeConfiguration<ShippingType>
    {
        public void Configure(EntityTypeBuilder<ShippingType> builder)
        {
            builder.Property(t => t.Price).HasColumnType("decimal(18,2)");
        }
    }
}