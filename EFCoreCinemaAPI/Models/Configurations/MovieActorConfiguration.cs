using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            //Primary key is a composite key of MovieId and ActorId
            builder.HasKey(ma => new { ma.MovieId, ma.ActorId });
            builder.Property(ma => ma.Character)
                .HasMaxLength(150);

            //relaciones de muchos a muchos

            builder.HasOne(ma=>ma.Actor)
                    .WithMany(a => a.MoviesActors) //un actor puede tener muchas peliculas
                    .HasForeignKey(ma => ma.ActorId); //la clave foranea de MovieActor es ActorId

            builder.HasOne(ma => ma.Movie)
                    .WithMany(m => m.MoviesActors) //una pelicula puede tener muchos actores
                    .HasForeignKey(ma => ma.MovieId); //la clave foranea de MovieActor es MovieId
        }
    }
}
