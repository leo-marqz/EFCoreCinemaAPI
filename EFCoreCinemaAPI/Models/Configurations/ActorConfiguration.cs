using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasField("_name").HasMaxLength(25).IsRequired();
            builder.Property(a => a.Biography).IsRequired();
            builder.Property(a => a.DateOfBirth).HasColumnType("date");
        }
    }
}
