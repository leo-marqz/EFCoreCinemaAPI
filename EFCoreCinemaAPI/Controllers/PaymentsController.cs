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
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        [HttpGet("paypal")]
        public async Task<ActionResult<IEnumerable<PaypalPay>>> GetPaypalPays()
        {
            return await _context.Payments.OfType<PaypalPay>().ToListAsync();
        }

        [HttpGet("card")]
        public async Task<ActionResult<IEnumerable<CardPay>>> GetCardPays()
        {
            return await _context.Payments.OfType<CardPay>().ToListAsync();
        }
    }
}
