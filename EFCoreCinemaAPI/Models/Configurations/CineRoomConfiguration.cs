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
                .HasDefaultValue(CineRoomType.CRT_2D);
        }
    }
}
