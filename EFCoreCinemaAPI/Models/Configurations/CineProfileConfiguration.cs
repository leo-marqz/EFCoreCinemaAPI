using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class CineProfileConfiguration : IEntityTypeConfiguration<CineProfile>
    {
        public void Configure(EntityTypeBuilder<CineProfile> builder)
        {
            //con esto le decimos a EF Core que CineProfile es parte de la tabla Cines
            // pero representado desde una entidad aparte, lo que cual nos ayuda a evitar
            //uso de select ya que esto se aplicara para informacion poco llamada
            builder.ToTable("Cines");
        }
    }
}
