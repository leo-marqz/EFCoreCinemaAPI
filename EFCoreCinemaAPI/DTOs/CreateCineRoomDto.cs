using EFCoreCinemaAPI.Models;

namespace EFCoreCinemaAPI.DTOs
{
    public class CreateCineRoomDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CineRoomType? CineRoomType { get; set; } = Models.CineRoomType.CRT_2D;
    }
}
