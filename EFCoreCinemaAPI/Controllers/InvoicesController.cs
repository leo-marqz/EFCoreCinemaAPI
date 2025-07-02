using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("scalar/function/get-total-by-invoice/all")]
        public async Task<ActionResult> GetTotalByInvoiceId()
        {
            var invoices = await _context.Invoices.Select((i)=>new
            {
                i.Id,
                Total = _context.GetInvoiceTotal(i.Id)
            })
            .OrderByDescending(i => i.Total)
            .ToListAsync();

            return Ok(invoices);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var invoice = new Invoice()
                {
                    TransactionDate = DateTime.Now
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                var items = new List<InvoiceDetail>()
                {
                    new InvoiceDetail()
                    {
                        InvoiceId = invoice.Id,
                        Product = "Product A",
                        Price = 14
                    },
                    new InvoiceDetail()
                    {
                        InvoiceId = invoice.Id,
                        Product = "Product B",
                        Price = 200
                    }
                };

                _context.InvoiceDetails.AddRange(items);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception e)
            {
                //await transaction.RollbackAsync();

                return StatusCode(500, "Error: " + e.Message);
            }
        }
    }
}
