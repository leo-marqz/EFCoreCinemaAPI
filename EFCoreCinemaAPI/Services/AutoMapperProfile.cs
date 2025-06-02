using AutoMapper;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;

namespace EFCoreCinemaAPI.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Actor, ActorDTO>();
            //dto - entity
            CreateMap<Cine, CineDTO>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(prop=>prop.Location.Y) )
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(prop=>prop.Location.X));
        }
    }
}
