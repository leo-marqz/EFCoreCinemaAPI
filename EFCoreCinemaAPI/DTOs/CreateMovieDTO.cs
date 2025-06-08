using System;

namespace EFCoreCinemaAPI.DTOs
{
    public class CreateMovieDto
    {
        public string Title { get; set; } 
        public bool IsOnSchedule { get; set; } 
        public string PosterUrl { get; set; } 
        public DateTime ReleaseDate { get; set; }

    }
}
