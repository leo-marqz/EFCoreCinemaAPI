using System;
using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models
{
    public class Movie
    {
        public int Id { get; set; } // Identificador de la Pelicula
        public string Title { get; set; } // Titulo de la Pelicula
        public bool IsOnSchedule { get; set; } // Indica si la Pelicula esta en cartelera o no
        public string PosterUrl { get; set; } // URL de la imagen del poster de la Pelicula
        public DateTime ReleaseDate { get; set; } // Fecha de estreno de la Pelicula

        public HashSet<Genre> Genres { get; set; } //Generos de Peliculas
        public HashSet<CineRoom> CineRooms { get; set; }
        public HashSet<MovieActor> MoviesActors { get; set; }
    }
}
