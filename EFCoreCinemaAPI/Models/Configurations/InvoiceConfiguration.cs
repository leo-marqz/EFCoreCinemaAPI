using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            // Configuración de la tabla Invoice como tabla de auditoría o temporal
            //builder.ToTable("Invoices", (options) =>
            //{
            //    options.IsTemporal((tb) =>
            //    {
            //        tb.HasPeriodStart(propertyName: "Desde");
            //        tb.HasPeriodEnd(propertyName: "Hasta");
            //        tb.UseHistoryTable(name: "Invoices_History");
            //    });
            //});

            //builder.Property("Desde").HasColumnName("Desde").HasColumnType("datetime2");
            //builder.Property("Hasta").HasColumnName("Hasta").HasColumnType("datetime2");

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
