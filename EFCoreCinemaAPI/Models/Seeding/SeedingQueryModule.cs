using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models.Seeding
{
    //Se usara para cargar datos iniciales en la base de datos
    public static class SeedingQueryModule
    {
        public static void Seed(ModelBuilder builder)
        {
            var action = new Genre { Id = 1, Name = "Action" };
            var comedy = new Genre { Id = 2, Name = "Comedy" };
            var drama = new Genre { Id = 3, Name = "Drama" };
            var horror = new Genre { Id = 4, Name = "Horror" };
            var romance = new Genre { Id = 5, Name = "Romance" };
            var animation = new Genre { Id = 6, Name = "Animation" };

            builder.Entity<Genre>()
                .HasData(action, comedy, drama, horror, romance, animation);

            var tomHolland = new Actor { 
                Id = 1, 
                Name = "Tom Holland", 
                Biography = "Thomas Stanley Holland (Kingston upon Thames, Londres; 1 de junio de 1996), conocido simplemente como Tom Holland, es un actor, actor de voz y bailarín británico.",
                DateOfBirth = new DateTime(1996, 6, 1) 
            };
            var robertDowneyJr = new Actor { 
                Id = 2, 
                Name = "Robert Downey Jr.", 
                Biography = "Robert John Downey Jr. (Nueva York; 4 de abril de 1965) es un actor, productor y cantante estadounidense.",
                DateOfBirth = new DateTime(1965, 4, 4) 
            };
            var scarlettJohansson = new Actor { 
                Id = 3, 
                Name = "Scarlett Johansson", 
                Biography = "Scarlett Ingrid Johansson (Nueva York; 22 de noviembre de 1984) es una actriz, cantante y modelo estadounidense.",
                DateOfBirth = new DateTime(1984, 11, 22) 
            };
            var chrisEvans = new Actor { 
                Id = 4, 
                Name = "Chris Evans", 
                Biography = "Christopher Robert Evans (Boston; 13 de junio de 1981) es un actor y director estadounidense.",
                DateOfBirth = new DateTime(1981, 6, 13) 
            };

            builder.Entity<Actor>()
                .HasData(tomHolland, robertDowneyJr, scarlettJohansson, chrisEvans);

            //4326: WGS 84, un sistema de coordenadas geográficas utilizado comúnmente
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //Country: El Salvador
            var cinemax = new Cine
            {
                Id = 1,
                Name = "Cinemax",
                // Address: Avenida Las Americas, San Salvador, El Salvador
                Location = geometryFactory.CreatePoint(new Coordinate(-89.1858, 13.7942))
            };

            var cineOfferCinemax = new CineOffer
            {
                Id = 1,
                DiscountPercentage = 10,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(30),
                CineId = cinemax.Id
            };

            var cineRoomCinemax2D = new CineRoom
            {
                Id = 1,
                Price = 9.99m,
                CineId = cinemax.Id,
                CineRoomType = CineRoomType.CRT_2D
            };

            var cineRoomCinemax3D = new CineRoom
            {
                Id = 2,
                Price = 12.99m,
                CineId = cinemax.Id,
                CineRoomType = CineRoomType.CRT_3D
            };

            var cineRoomCinemaxCXC = new CineRoom
            {
                Id = 3,
                Price = 15.99m,
                CineId = cinemax.Id,
                CineRoomType = CineRoomType.CRT_CXC
            };
            
            
            builder.Entity<Cine>().HasData(cinemax);

            builder.Entity<CineOffer>().HasData(cineOfferCinemax);
            builder.Entity<CineRoom>()
                .HasData(cineRoomCinemax2D, cineRoomCinemax3D, cineRoomCinemaxCXC);

            var avengersEndGame = new Movie
            {
                Id = 1,
                Title = "Avengers: Endgame",
                ReleaseDate = new DateTime(2019, 4, 26),
                IsOnSchedule = true,
                PosterUrl = "https://example.com/posters/avengers-endgame.jpg"
            };

            var spiderManNoWayHome = new Movie
            {
                Id = 2,
                Title = "SpiderMan: No Way Home",
                ReleaseDate = new DateTime(2021, 12, 17),
                IsOnSchedule = true,
                PosterUrl = "https://example.com/posters/spiderman-no-way-home.jpg"
            };

            var blackWidow = new Movie
            {
                Id = 3,
                Title = "Black Widow",
                ReleaseDate = new DateTime(2021, 7, 9),
                IsOnSchedule = false,
                PosterUrl = "https://example.com/posters/black-widow.jpg"
            };

            builder.Entity<Movie>()
                .HasData(avengersEndGame, spiderManNoWayHome, blackWidow);

        }   
        
    }
}
