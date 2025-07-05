using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        [HttpGet("temporal-all/genre/{id:int}")]
        public async Task<ActionResult> GetAllTemporalAll(int id)
        {
            //Consultando todo el historico de un género específico por ID
            var genres = await _context.Genres
                .TemporalAll() // Retrieves all temporal data
                .AsTracking() // Ensures that the entity is tracked by the context
                .Where(g => g.Id == id)
                .Select((g) =>new
                {
                    Id = g.Id,
                    Name = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd"),
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet("temporal-as-of/genre/{id:int}")]
        public async Task<ActionResult> GetTemporalAsOf(int id, DateTime date)
        {
            //Consultando el historico de un genero específico por ID en una fecha específica
            var genres = await _context.Genres
                .TemporalAsOf(date) 
                .AsTracking()
                .Where(g => g.Id == id)
                .Select((g) => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd"),
                })
                .ToListAsync();
            return Ok(genres);
        }

        [HttpGet("temporal-for-to/genre/{id:int}")]
        public async Task<ActionResult> TemporalFromToByGenreId(int id, DateTime from, DateTime to)
        {
            //Consultando el historico de un género específico por ID en un rango de fechas
            var genres = await _context.Genres
                .TemporalFromTo(from, to)
                .AsTracking() 
                .Where(g => g.Id == id)
                .Select((g) => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd"),
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet("temporal-contain-in/genre/{id:int}")]
        public async Task<ActionResult> TemporalContainInByGenreId(int id, DateTime from, DateTime to)
        {
            // Recuperando el historico de un genero específico por ID
            // contenido entre dos fechas (los segundos son importantes)
            var genres = await _context.Genres
                .TemporalContainedIn(from, to)
                .AsTracking()
                .Where(g => g.Id == id)
                .Select((g) => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd"),
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get()
        {
            try
            {
                _context.Logs.Add( new Log { Id = Guid.NewGuid(), Message = "Fetching all genres - GenresController.Get()"} );
                await _context.SaveChangesAsync();

                // Retrieve all genres from the database (Simple query)
                // Using AsNoTracking for read-only queries to improve performance
                // AsTracking: follows change tracking, which is useful for updates
                //return await _context.Genres.AsNoTracking().ToListAsync();
                var data = await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }

        [HttpGet("obtener-propiedad-sombra")]
        public async Task<ActionResult<Genre>> GetShadowProperty(int id)
        {
            try
            {
                if(id <= 0) return BadRequest("Invalid ID provided.");
                // Retrieve a specific genre by ID from the database
                var genre = await _context.Genres.AsTracking().FirstOrDefaultAsync(g => g.Id == id);
                // If genre is not found, return NotFound
                if (genre is null) return NotFound($"Genre with ID {id} not found.");
                // Accessing shadow property "CreatedAt"
                var createdAt = _context.Entry(genre).Property<DateTime>("CreatedAt").CurrentValue;
                // Returning the genre along with its shadow property
                return Ok(new { Genre = genre, CreatedAt = createdAt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }

        [HttpGet("ordenando-elementos-con-fecha-creacion")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetOrderByCreatedAt()
        {
            //obteniendo los generos ordenados por la fecha de creación (propiedad sombra)
            return await _context.Genres
                .OrderByDescending(gr => EF.Property<DateTime>(gr, "CreatedAt"))
                .ToListAsync();
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
                var genre = await _context.Genres.FirstOrDefaultAsync((g)=>g.Name.Contains(name));
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

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<Genre>>> Pagination(int page = 1)
        {
            var recordsByPage = 3;
            var pageIndex = (page - 1) * recordsByPage;
            var genres = await _context.Genres
                                    .Skip(pageIndex)
                                    .Take(recordsByPage)
                                    .ToListAsync();
            return Ok(genres);
        }

        [HttpGet("queriable/sql/{id:int}")]
        public async Task<ActionResult<Genre>> GetGenreUsingQueries(int id)
        {
            //Es obligatorio usar Select * From Genres, de lo contrario no funciona
            var genre = await _context.Genres
                                .FromSqlRaw("SELECT * FROM Genres WHERE Id = {0}", id)
                                .IgnoreQueryFilters() // Ignoring global query filters
                                .FirstOrDefaultAsync();
            if(genre is null)
            {
                return NotFound($"Genre with ID {id} not found.");
            }

            var createdAt = _context.Entry(genre).Property<DateTime>("CreatedAt").CurrentValue;

            return Ok(new { Genre = genre, CreatedAt = createdAt });
        }

        [HttpGet("queriable/sql/string/interpolation{id:int}")]
        public async Task<ActionResult<Genre>> GetGenreUsingStringInterpolation(int id)
        {
            //Es obligatorio usar Select * From Genres, de lo contrario no funciona
            var genre = await _context.Genres
                                .FromSqlInterpolated($"SELECT * FROM Genres WHERE Id = {id}")
                                .IgnoreQueryFilters() // Ignoring global query filters
                                .FirstOrDefaultAsync();
            if(genre is null)
            {
                return NotFound($"Genre with ID {id} not found.");
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> Post(Genre genre)
        {
            if(genre is null)
            {
                return BadRequest("Genre cannot be null.");
            }

            var existingGenre = await _context.Genres
                                            .FirstOrDefaultAsync(g => g.Name == genre.Name);

            if (existingGenre != null)
            {
                return BadRequest($"Genre with name '{genre.Name}' already exists.");
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Genres (Name) VALUES ({genre.Name})");

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("create-genre-with-stored-procedure")]
        public async Task<ActionResult> CreateGenreWithStoredProcedure(Genre genre)
        {
            if(genre is null)
            {
                return BadRequest("Genre cannot be null.");
            }

            var existingGenre = await _context.Genres
                                            .FirstOrDefaultAsync(g => g.Name == genre.Name);
            

            if (existingGenre != null)
            {
                return BadRequest($"Genre with name '{genre.Name}' already exists.");
            }
            
            var outputId = new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            // Call the stored procedure to create a new genre
            await _context.Database
                .ExecuteSqlRawAsync(
                    "EXEC Genre_InsertNewGenre @Name = {0}, @Id = {1} OUTPUT", 
                    genre.Name, outputId
                );

            var id = (int)outputId.Value;

            return Ok(id);
        }

        [HttpGet("get-genre-using-stored-procedure/{id:int}")]
        public async Task<ActionResult<Genre>> GetGenreUsingStoredProcedure(int id)
        {
            var genres = _context.Genres
                                .FromSqlInterpolated($"EXEC Genre_GetGenreById @Id = {id}")
                                .IgnoreQueryFilters() // Ignoring global query filters
                                .AsAsyncEnumerable();

            await foreach(var genre in genres)
            {
                return Ok(genre);
            }

            return NotFound($"Genre with ID {id} not found.");
        }

    }
}
