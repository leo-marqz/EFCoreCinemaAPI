using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            //Columna calculada para Total
            builder.Property(x => x.Total)
                .HasComputedColumnSql("[Price] * [Quantity]");
                //.HasComputedColumnSql("[Price] * [Quantity]", stored: true);
        }
    }
}
