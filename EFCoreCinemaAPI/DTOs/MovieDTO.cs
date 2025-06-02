using System.Collections.Generic;

namespace EFCoreCinemaAPI.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GenreDTO> Genres { get; set; }
        public ICollection<CineDTO> Cines { get; set; }
        public ICollection<ActorDTO> Actores { get; set; }
    }
}
