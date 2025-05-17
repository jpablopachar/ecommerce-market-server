using Core.Entities.PurchaseOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessLogic.Data.Configuration
{
    /// <summary>
    /// Configuración de la entidad <see cref="ItemOrder"/> para el contexto de datos.
    /// </summary>
    /// <remarks>
    /// Esta clase implementa <see cref="IEntityTypeConfiguration{TEntity}"/> para definir la configuración de la entidad <see cref="ItemOrder"/>.
    /// </remarks>
    public class ItemOrderConfiguration : IEntityTypeConfiguration<ItemOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOrder> builder)
        {
            builder.OwnsOne(i => i.OrderedItem, x => { x.WithOwner(); });
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
        }
    }
}