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
                                    .Include(m => m.Genres)
                                    .Include(m => m.CineRooms) // Incluir las salas de cine asociadas
                                        .ThenInclude(cr=>cr.Cine) // Incluir el cine asociado a cada sala
                                    .Include(m => m.MoviesActors) // Incluir los actores asociados a la película
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
    }
}
