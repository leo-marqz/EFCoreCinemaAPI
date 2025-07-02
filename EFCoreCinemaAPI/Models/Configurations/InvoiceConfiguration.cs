using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasMany(typeof(InvoiceDetail)).WithOne();

            builder.Property(i => i.InvoiceNumber)
                .HasDefaultValueSql("NEXT VALUE FOR Invoice.InvoiceNumber");
        }
    }
}
