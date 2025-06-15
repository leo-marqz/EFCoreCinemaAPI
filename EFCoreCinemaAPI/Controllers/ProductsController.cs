using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("merchs")]
        public async Task<ActionResult<IEnumerable<Merchandising>>> GetMerchandising()
        {
            return await _context.Set<Merchandising>().ToListAsync();
        }

        //GET: api/products/laptops
        [HttpGet("laptops")]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops()
        {
            return await _context.Set<Laptop>().ToListAsync();
        }
    }
}
