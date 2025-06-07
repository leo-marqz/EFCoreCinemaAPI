using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual HashSet<Movie> Movies { get; set; }
    }
}
