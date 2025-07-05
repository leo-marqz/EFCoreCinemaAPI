using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasMany(typeof(InvoiceDetail)).WithOne();

            // Nos sirve para detectar conflictos de concurrencia
            // cuando dos usuarios intentan actualizar la misma factura al mismo tiempo.
            // Si una factura es actualizada por un usuario, y otro usuario intenta actualizarla
            // al mismo tiempo, se lanzará una excepción de concurrencia.
            builder.Property(i => i.Version).IsRowVersion();

            builder.Property(i => i.InvoiceNumber)
                .HasDefaultValueSql("NEXT VALUE FOR Invoice.InvoiceNumber");
        }
    }
}
