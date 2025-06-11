using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models
{
    public class CineRoom
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CineRoomType? CineRoomType { get; set; } = Models.CineRoomType.CRT_2D;

        public Currency Currency { get; set; } = Currency.USD;

        public int CineId { get; set; }
        public Cine Cine { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
