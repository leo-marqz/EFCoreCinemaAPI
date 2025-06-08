using System.Collections.Generic;

namespace EFCoreCinemaAPI.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
        public ICollection<CineDto> Cines { get; set; } = new List<CineDto>();
        public ICollection<ActorDto> Actores { get; set; } = new List<ActorDto>();
    }
}
