using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
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
        public async Task<ActionResult<IEnumerable<ActorDto>>> Get()
        {
            var actors = await _context.Actors
                            .Select(a => new ActorDto
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
                .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(actors);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateActorDto request)
        {
            var actor = _mapper.Map<Actor>(request);
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdateActorDto updateActorDto)
        {
            var actordb = await _context.Actors.AsTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (actordb == null)
            {
                return NotFound();
            }
            //aqui mapper no crea un nuevo objeto, sino que actualiza el existente (actordb)
            actordb = _mapper.Map(updateActorDto, actordb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("objeto-desconectado/{id:int}")]
        public async Task<ActionResult> PutDisconnected(int id, UpdateActorDto updateActorDto)
        {
            var actordb = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actordb is null)
            {
                return NotFound();
            }

            //objeto desconectado - se actualizado todo - diferente al modelo conectado
            var actor = _mapper.Map<Actor>(updateActorDto);

            actor.Id = id; //asignar el id al objeto desconectado
            //marcar como modificado
            //_context.Entry(actor).State = EntityState.Modified;
            _context.Update(actor); //esto marca el objeto como modificado

            //otra forma de actualizar es por actualizacion por propiedades
            //_context.Entry(actor).Property(a => a.Name).IsModified = true;

            await _context.SaveChangesAsync();
            return Ok(actor);
        }
    }
}
