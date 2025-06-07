using System;
using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Se agrega virtual para permitir la carga diferida (lazy loading) o carga perezosa
        // Se instalo el paquete Microsoft.EntityFrameworkCore.Proxies para habilitar esta funcionalidad
        // Esto permite que las propiedades de navegación se carguen automáticamente cuando se accede a ellas por primera vez
        public virtual HashSet<MovieActor> MoviesActors { get; set; }
    }
}
