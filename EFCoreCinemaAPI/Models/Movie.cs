using System;

namespace EFCoreCinemaAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsOnSchedule { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
