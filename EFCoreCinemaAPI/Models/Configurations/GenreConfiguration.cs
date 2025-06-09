using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g=>g.Name)
                .HasMaxLength(25)
                .IsRequired();

            //Nos ayudara a evitar que se muestren los géneros eliminados lógicamente
            builder.HasQueryFilter(g => !g.IsDeleted);
        }
    }
}
