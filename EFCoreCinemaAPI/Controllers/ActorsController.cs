using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCinemaAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("using-select")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> Get()
        {
            var actors = await _context.Actors
                            .Select(a=> new ActorDTO
                            {
                                Id = a.Id,
                                Name = a.Name
                            })
                            .ToListAsync();
            return Ok(actors);
        }

        [HttpGet("using-automapper")]
        public async Task<ActionResult> Gets()
        {
            var actors = await _context.Actors
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(actors);
        }
    }
}
