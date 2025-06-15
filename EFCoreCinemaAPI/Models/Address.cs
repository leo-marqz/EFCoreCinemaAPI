using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCinemaAPI.Models
{
    //esto es una clase que no sera mapeada a la base de datos
    //independientemente de que se use en otras entidades
    //[NotMapped]

    //aplicaremos otro concepto: Entidad de Propiedad
    [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
