using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCinemaAPI.Models
{
    //Con [Index] se crea un indice en la base de datos para el campo Name
    // y se asegura que sea único, es decir, no puede haber dos géneros con el mismo nombre.
    //[Index(nameof(Name), IsUnique = true)]
    public class Genre : AuditableEntity
    {
        public int Id { get; set; }

        [ConcurrencyCheck] // Esto asegura que el campo Name se maneje correctamente en caso de concurrencia
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
