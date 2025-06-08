using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreCinemaAPI.Models
{
    public class Actor
    {
        //Mapeo Flexible: Se puede usar un campo privado para la propiedad Name
        private string _name;

        public int Id { get; set; }
        public string Name {
            get { return _name; }
            set
            {
                _name = string.Join(' ',
                                value.Split(' ')
                                .Select((word) => char.ToUpper(word[0]) + word.Substring(1).ToLower())
                                .ToArray()
                            );
                                
            } 
        }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Se agrega virtual para permitir la carga diferida (lazy loading) o carga perezosa
        // Se instalo el paquete Microsoft.EntityFrameworkCore.Proxies para habilitar esta funcionalidad
        // Esto permite que las propiedades de navegación se carguen automáticamente cuando se accede a ellas por primera vez
        public virtual HashSet<MovieActor> MoviesActors { get; set; }
    }
}
