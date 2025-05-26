using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        public GenresController()
        {
            
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok();
        }
    }
}
