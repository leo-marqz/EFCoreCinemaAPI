using AutoMapper;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
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

            //create cine (full object)
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            CreateMap<CreateCineDto, Cine>()
                .ForMember( (ent) => ent.Location,
                            (dto) => dto.MapFrom(
                                     (prop) => geometryFactory.CreatePoint(new Coordinate(prop.Longitude, prop.Latitude)))
                            );
            CreateMap<CreateCineOfferDto, CineOffer>();
            CreateMap<CreateCineRoomDto, CineRoom>();

            CreateMap<CreateMovieDto, Movie>()
                .ForMember(
                            (ent) => ent.Genres,
                            (dto) => dto.MapFrom((prop) => prop.Genres.Select((id) => new Genre { Id = id }))
                           )
                .ForMember(
                            (ent) => ent.CineRooms,
                            (dto) => dto.MapFrom((prop) => prop.CineRooms.Select((id) => new CineRoom { Id = id })));

            CreateMap<CreateMovieActorDto, MovieActor>();

            CreateMap<CreateActorDto, Actor>();
            CreateMap<UpdateActorDto, Actor>();
        }
    }
}
