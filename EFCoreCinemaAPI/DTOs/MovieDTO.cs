using System.Collections.Generic;

namespace EFCoreCinemaAPI.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public ICollection<CineDTO> Cines { get; set; } = new List<CineDTO>();
        public ICollection<ActorDTO> Actores { get; set; } = new List<ActorDTO>();
    }
}
