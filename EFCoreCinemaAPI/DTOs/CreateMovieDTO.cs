using System;
using System.Collections.Generic;

namespace EFCoreCinemaAPI.DTOs
{
    public class CreateMovieDto
    {
        public string Title { get; set; } 
        public bool IsOnSchedule { get; set; } 
        public string PosterUrl { get; set; } 
        public DateTime ReleaseDate { get; set; }

        public List<int> Genres { get; set; }
        public List<int> CineRooms { get; set; }
        public List<CreateMovieActorDto> MoviesActors { get; set; }

    }
}
