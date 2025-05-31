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
        }
    }
}
