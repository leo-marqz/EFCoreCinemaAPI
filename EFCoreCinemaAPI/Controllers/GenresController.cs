using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get()
        {
            try
            {
                // Retrieve all genres from the database (Simple query)
                // Using AsNoTracking for read-only queries to improve performance
                // AsTracking: follows change tracking, which is useful for updates
                //return await _context.Genres.AsNoTracking().ToListAsync();
                var data = await _context.Genres.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            try
            {
                if(id <= 0) return BadRequest("Invalid ID provided.");

                // Retrieve a specific genre by ID from the database
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

                // If genre is not found, return NotFound
                if (genre is null) return NotFound($"Genre with ID {id} not found.");

                // If genre is found, return it with a 200 OK status
                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }

        [HttpGet("search/{name}/first")]
        public async Task<ActionResult<Genre>> FindByName(string name)
        {
            try
            {
                if(name.IsNullOrEmpty()) return BadRequest("Name cannot be null or empty.");
                var genre = await _context.Genres.FirstOrDefaultAsync(
                        (g)=>g.Name.Contains(name)
                    );
                if (genre is null) return NotFound($"Genre with Name {name} not found!");

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Genre>>> Search(string search, [FromQuery] string orderBy)
        {
            try
            {
                if(search.IsNullOrEmpty()) 
                    return BadRequest("Search term cannot be null or empty.");

                List<Genre> genres = new List<Genre>();

                if ( !orderBy.IsNullOrEmpty() && orderBy.ToLower() == "desc")
                {
                    genres = await _context.Genres
                        .Where(g => g.Name.Contains(search) )
                        .OrderByDescending(g=>g.Name)
                        .ToListAsync();
                }else
                {
                    genres = await _context.Genres
                        .Where(g => g.Name.Contains(search))
                        .OrderBy(g => g.Name)
                        .ToListAsync();
                }


                if (genres.Count == 0) return NotFound($"No genres found matching '{search}'.");
                
                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }
    }
}
