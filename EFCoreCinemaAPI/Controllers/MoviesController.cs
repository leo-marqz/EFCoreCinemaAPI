using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("eager-loading/{id:int}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            //usar DTO para evitar el ciclo infinito debido a propiedades de navegacion
            //tambien se puede hacer una confiracion en Program sobre AddControllers()
            var movie = await _context.Movies
                                    .Include(m => m.Genres.OrderByDescending(g => g.Name))
                                    .Include(m => m.CineRooms) // Incluir las salas de cine asociadas
                                        .ThenInclude(cr => cr.Cine) // Incluir el cine asociado a cada sala
                                    .Include(m => m.MoviesActors.Where(pa => pa.Actor.DateOfBirth.Value.Year > 1980)) // Incluir los actores asociados a la película
                                        .ThenInclude(ma => ma.Actor) // Incluir los actores asociados a la película
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDto>(movie);

            movieDTO.Cines = movieDTO.Cines.DistinctBy(c => c.Id).ToList(); // Eliminar cines duplicados

            return Ok(movie);
        }

        //projectTo e Eager Loading
        [HttpGet("withProjectTo/{id:int}")]
        public async Task<ActionResult> GetByIdUsingProjectTo(int id)
        {
            var movie = await _context.Movies.ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync(mv => mv.Id == id);

            if (movie is null) return NotFound();

            movie.Cines = movie.Cines.DistinctBy(x => x.Id).ToList();

            return Ok(movie);
        }

        [HttpGet("using-select/{id:int}")]
        public async Task<ActionResult> GetByIdUsingSelect(int id)
        {
            var movie = await _context.Movies.Select((mv) => new
            {
                Id = mv.Id,
                Title = mv.Title,
                Genres = mv.Genres.OrderBy((gr) => gr.Name).ToList(),
                ActorsCount = mv.MoviesActors.Count(),
                CinesCount = mv.CineRooms.Select((cr) => cr.CineId).Distinct().Count()
            }).FirstOrDefaultAsync((mv) => mv.Id == id);

            if (movie is null) return NotFound();

            return Ok(movie);
        }

        [HttpGet("explicit-loading/{id:int}")]
        public async Task<ActionResult> GetByIdWithExplicitLoad(int id)
        {
            var movie = await _context.Movies.AsTracking().FirstOrDefaultAsync((mv) => mv.Id == id);
            await _context.Entry(movie).Collection((mv) => mv.Genres).LoadAsync();

            if (movie is null) return NotFound();

            var genres = await _context.Entry(movie).Collection((mv) => mv.Genres).Query().CountAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return Ok(new
            {
                movie = movieDto,
                genresCount = genres
            });
        }

        [HttpGet("lazy-loading/{id:int}")]
        public async Task<ActionResult> GetByIdWithLazyLoading(int id)
        {
            var movie = await _context.Movies.AsTracking()
                                .FirstOrDefaultAsync((mv) => mv.Id == id);
            if(movie is null)
            {
                return NotFound();
            }

            // Lazy loading requires virtual navigation properties in the model
            // se activa el lazy loading cuando se detecta que se necesitan los datos relacionados
            // en este MovieDTO, las propiedades de navegación son virtuales
            var movieDto = _mapper.Map<MovieDto>(movie);

            // Lazy loading will automatically load the related entities when accessed
            movieDto.Cines = movieDto.Cines.DistinctBy(c => c.Id).ToList(); 

            return Ok(movieDto);
        }

        [HttpGet("group-by-is-on-schedule")]
        public async Task<ActionResult> GetGroupByReleaseDate()
        {
            var movies = await _context.Movies.GroupBy((mv)=>mv.IsOnSchedule)
                                    .Select((mv)=>new
                                    {
                                        IsOnSchedule = mv.Key,
                                        Count = mv.Count(),
                                        Movies = mv.ToList()
                                    }).ToListAsync();
            return Ok(movies);
        }

        [HttpGet("group-by-genres")]
        public async Task<ActionResult> GetGroupByGenres()
        {
            var movies = await _context.Movies.GroupBy((mv)=> mv.Genres.Count())
                                    .Select((mv)=>new
                                    {
                                        Count = mv.Key,
                                        Title = mv.Select((mv)=>mv.Title),
                                        Genre = mv.Select((mv)=>mv.Genres).SelectMany((gr)=>gr)
                                                    .Select((gr)=>gr.Name)
                                    }).ToListAsync();
            return Ok(movies);
        }

        [HttpGet("filter")]
        public async Task<ActionResult> FilterMovies([FromQuery] MovieFiltersDto filters)
        {
            var moviesQueryable = _context.Movies.AsQueryable();

            if(filters.GenreId > 0)
            {
                moviesQueryable = moviesQueryable
                                        .Where((mv) => mv.Genres.Select((gr) => gr.Id)
                                        .Contains(filters.GenreId));
            }

            if (!string.IsNullOrEmpty(filters.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(filters.Title));
            }

            //En Cartelera o no
            if (filters.IsOnSchedule)
            {
                moviesQueryable = moviesQueryable.Where((mv) => mv.IsOnSchedule);
            }

            //Proximos Estrenos
            if (filters.IsUpcomingRelease)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where((mv) => mv.ReleaseDate > today);
            }

            var movies = await moviesQueryable.Include((mv)=>mv.Genres)
                                    .ToListAsync();

            return Ok( _mapper.Map<List<MovieDto>>(movies) );
        }
    }
}
