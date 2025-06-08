using System;

namespace EFCoreCinemaAPI.DTOs
{
    public class MovieFiltersDto
    {
        public int GenreId { get; set; }
        public string Title { get; set; }
        public bool IsOnSchedule { get; set; }
        public bool IsUpcomingRelease { get; set; }
    }
}
