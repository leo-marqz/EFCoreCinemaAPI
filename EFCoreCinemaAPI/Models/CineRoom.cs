using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models
{
    public class CineRoom
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CineRoomType? CineRoomType { get; set; } = Models.CineRoomType.CRT_2D;

        public int CineId { get; set; }
        public virtual Cine Cine { get; set; }

        public virtual HashSet<Movie> Movies { get; set; }
    }
}
