using AutoMapper;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            //usar DTO para evitar el ciclo infinito debido a propiedades de navegacion
            //tambien se puede hacer una confiracion en Program sobre AddControllers()
            var movie = await _context.Movies
                                    .Include(m => m.Genres.OrderByDescending(g=>g.Name))
                                    .Include(m => m.CineRooms) // Incluir las salas de cine asociadas
                                        .ThenInclude(cr=>cr.Cine) // Incluir el cine asociado a cada sala
                                    .Include(m => m.MoviesActors.Where(pa=>pa.Actor.DateOfBirth.Value.Year > 1980)) // Incluir los actores asociados a la película
                                        .ThenInclude(ma => ma.Actor) // Incluir los actores asociados a la película
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if(movie is null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            movieDTO.Cines = movieDTO.Cines.DistinctBy(c => c.Id).ToList(); // Eliminar cines duplicados

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Post(CreateMovieDTO request)
        {
            if (request is null) return NotFound();
            var movie = new Movie
            {
                Title = request.Title,
                IsOnSchedule = request.IsOnSchedule,
                PosterUrl = request.PosterUrl,
                ReleaseDate = request.ReleaseDate
            };

            var result = await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            var movieDTO = _mapper.Map<MovieDTO>(result);

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movieDTO);

        }
    }
}
