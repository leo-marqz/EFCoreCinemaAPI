using System;

namespace EFCoreCinemaAPI.DTOs
{
    public class UpdateActorDto
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
