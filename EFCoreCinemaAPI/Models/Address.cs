using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCinemaAPI.Models
{
    //esto es una clase que no sera mapeada a la base de datos
    //independientemente de que se use en otras entidades
    [NotMapped]
    public class Address
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
