using Core.Entities.PurchaseOrder;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data.Configuration
{
    /// <summary>
    /// Configuración de la entidad <see cref="PurchaseOrder"/> para el contexto de datos.
    /// </summary>
    /// <remarks>
    /// Esta clase implementa <see cref="IEntityTypeConfiguration{TEntity}"/> para definir la configuración de la entidad <see cref="PurchaseOrder"/>.
    /// </remarks>
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.OwnsOne(o => o.MailingAddress, x => { x.WithOwner(); });

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o!)
                );

            builder.HasMany(o => o.ItemsOrder).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}