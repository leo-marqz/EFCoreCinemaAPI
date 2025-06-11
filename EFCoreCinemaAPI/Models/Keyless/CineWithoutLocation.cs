using Microsoft.EntityFrameworkCore;

namespace EFCoreCinemaAPI.Models.Keyless
{
    //Configuramos de una entidad sin clave primaria (Keyless Entity)
    //[Keyless] // This attribute indicates that the entity does not have a primary key
    public class CineWithoutLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
