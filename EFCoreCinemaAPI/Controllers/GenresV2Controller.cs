using AutoMapper;
using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresV2Controller : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresV2Controller(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genre request)
        {
            //Estados de un objeto en EF Core:
            // 1. Detached: El objeto no está siendo rastreado por el contexto.
            // 2. Unchanged: El objeto está siendo rastreado y no ha cambiado desde que se cargó.
            // 3. Added: El objeto está siendo rastreado y se ha marcado para ser insertado en la base de datos.
            // 4. Modified: El objeto está siendo rastreado y se ha modificado desde que se cargó.
            // 5. Deleted: El objeto está siendo rastreado y se ha marcado para ser eliminado de la base de datos.

            var status = context.Entry(request).State; //Detached

            //marcado como agregado, 
            this.context.Add(request);

            //el estado del objeto se cambia a Added,
            var status2 = context.Entry(request).State; // Added

            await context.SaveChangesAsync();

            var status3 = context.Entry(request).State; // Unchanged

            return Ok(new
            {
                Status1 = status.ToString(),
                Status2 = status2.ToString(),
                Status3 = status3.ToString()
            });
        }

        [HttpPost("save-many")]
        public async Task<ActionResult> PostMany(Genre[] request)
        {
            context.AddRange(request);
            await context.SaveChangesAsync();
            return Ok(new
            {
                Count = request.Length,
                Message = "Genres added successfully."
            });
        }
    }
}
