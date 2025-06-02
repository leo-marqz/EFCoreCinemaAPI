using AutoMapper;
using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                                    .Include(m => m.CineRooms)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if(movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
    }
}
