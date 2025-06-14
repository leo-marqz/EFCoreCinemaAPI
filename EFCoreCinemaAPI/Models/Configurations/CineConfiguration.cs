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

            //si se usan las convenciones de EF Core,
            //no es necesario hacer esto, pero es una buena practica
            //prodiedad de navegacion, propiedad de llave primaria y clave foranea
            builder.HasMany(c=>c.CineRooms) //un cine tiene muchas salas
                .WithOne(cr => cr.Cine) //una sala pertenece a un cine
                .HasForeignKey(cr => cr.CineId); //la clave foranea de CineRoom es CineId
        }
    }
}
