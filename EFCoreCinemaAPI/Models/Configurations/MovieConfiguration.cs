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

            //relacion mucho a muchos entre generos y peliculas
            //esto se configuro usando las convenciones de EF Core
            //no se creo tabla intermedia explicita
            //builder.HasMany(mv => mv.Genres)
            //        .WithMany(gr => gr.Movies)
            //        .UsingEntity(config=>config.ToTable("GenresMovies") );
            //dentro de UsingEntity podemos configurar muchas cosas,
            //como el nombre de la tabla, las columnas, etc.
            //y carga de algunos datos
            //.UsingEntity(config=>{ config.ToTable("GenresMovies").HasData( new { ... } )  );
        }
    }
}
