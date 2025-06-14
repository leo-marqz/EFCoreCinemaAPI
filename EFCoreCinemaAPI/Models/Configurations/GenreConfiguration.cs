﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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
