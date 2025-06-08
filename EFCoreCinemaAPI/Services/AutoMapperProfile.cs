using AutoMapper;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
using System.Linq;

namespace EFCoreCinemaAPI.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Actor, ActorDto>();
            //dto - entity
            CreateMap<Cine, CineDto>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(prop=>prop.Location.Y) )
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(prop=>prop.Location.X));

            CreateMap<Genre, GenreDto>();

            //generos no se mapea por que es una relacion simple
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.CineRooms.Select(cr => cr.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(prop => prop.MoviesActors.Select(ma => ma.Actor)));
        }
    }
}
