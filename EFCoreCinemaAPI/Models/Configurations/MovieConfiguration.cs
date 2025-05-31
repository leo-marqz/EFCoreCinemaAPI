using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(m => m.PosterUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .IsRequired();
            builder.Property(m => m.ReleaseDate)
                .HasColumnType("date");
        }
    }
}
