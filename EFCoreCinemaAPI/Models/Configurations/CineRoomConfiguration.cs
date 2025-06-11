using EFCoreCinemaAPI.Models.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class CineRoomConfiguration : IEntityTypeConfiguration<CineRoom>
    {
        public void Configure(EntityTypeBuilder<CineRoom> builder)
        {
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Price)
                .HasPrecision(precision: 9, scale: 2)
                .IsRequired();
            builder.Property(cr => cr.CineRoomType)
                //.HasConversion<string>() // Convert CineRoomType enum to string for storage
                .HasDefaultValue(CineRoomType.CRT_2D);

            builder.Property((cr) => cr.Currency)
                //.HasDefaultValue(Currency.USD) // Default currency
                .HasConversion<CurrencySymbolConverter>();
        }
    }
}
