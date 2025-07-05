using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EFCoreCinemaAPI.Models.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            //Tablas temporales: Nos permite evar un historial de cambios en la entidad.
            //Esto es util para auditoria, o para mantener un historial de cambios
            //en la entidad sin necesidad de crear una tabla adicional.
            //Ademas, nos permite recuperar la entidad en un estado anterior
            builder.ToTable("Genres", (options) =>
            {
                options.IsTemporal();
            });

            builder.Property("PeriodStart").HasColumnType("datetime2");
            builder.Property("PeriodEnd").HasColumnType("datetime2");

            builder.HasKey(g => g.Id);
            builder.Property(g=>g.Name)
                .HasMaxLength(25)
                .IsRequired();

            // Con IsConcurrencyToken nos ayuda a detectar conflictos de concurrencia
            // cuando dos usuarios intentan actualizar el mismo género al mismo tiempo.
            // Si un género es actualizado por un usuario, y otro usuario intenta actualizarlo
            // al mismo tiempo, se lanzará una excepción de concurrencia.
            builder.Property((g)=>g.Name)
                   .IsConcurrencyToken();

            //Nos ayudara a evitar que se muestren los géneros eliminados lógicamente
            builder.HasQueryFilter(g => !g.IsDeleted);

            //Nos aseguramos de que el campo Name sea único en la base de datos
            // y que no se pueda insertar un género con el mismo nombre si ya existe uno con IsDeleted = false
            builder.HasIndex(g => g.Name).IsUnique().HasFilter("IsDeleted = 'false'");

            //propiedades sombra
            //Estas columnas se configuran aqui, pero no se definen en la clase Genre
            builder.Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GetDate()")
                .HasColumnType("datetime2")
                .IsRequired(true);

        }
    }
}
