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

            //ingorando la propiedad Address para que no se mapee a la base de datos
            //builder.Ignore(a => a.Address);

            //configurando Entidad de Propiedad (Owned Entity)
            builder.OwnsOne(a => a.HomeAddress, adr =>
            {
                adr.Property(ad => ad.Street).HasColumnName("Street");
                adr.Property(ad => ad.State).HasColumnName("State");
                adr.Property(ad => ad.Country).HasColumnName("Country");
            });
        }
    }
}
