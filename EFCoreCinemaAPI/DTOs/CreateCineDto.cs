using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCinemaAPI.DTOs
{
    public class CreateCineDto
    {
        [Required]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CreateCineOfferDto CineOffer { get; set; }
        public List<CreateCineRoomDto> CineRooms { get; set; }
    }
}
