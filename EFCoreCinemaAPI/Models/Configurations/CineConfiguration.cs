using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class CineConfiguration : IEntityTypeConfiguration<Cine>
    {
        public void Configure(EntityTypeBuilder<Cine> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasMaxLength(150)
                .IsRequired();

            //relacion uno a uno con CineOffer
            builder.HasOne(c => c.CineOffer) //un cine tiene una oferta
                .WithOne() //primera forma, ademas se usa asi si no se crease una propiedad de navegacion en CineOffer
                           //.WithOne(co => co.Cine) //una oferta tiene un solo cine
                .HasForeignKey<CineOffer>(co => co.CineId); //la clave foranea de CineOffer es CineId
        }
    }
}
