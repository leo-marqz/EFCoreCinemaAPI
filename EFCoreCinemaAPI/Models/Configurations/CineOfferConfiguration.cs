using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class CineOfferConfiguration : IEntityTypeConfiguration<CineOffer>
    {
        public void Configure(EntityTypeBuilder<CineOffer> builder)
        {
            builder.HasKey(co => co.Id);
            builder.Property(co => co.StartDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(co => co.EndDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(co => co.DiscountPercentage)
                .HasPrecision(precision: 5, scale: 2)
                .IsRequired();
        }
    }
}
