using Castle.Core.Logging;
using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(ApplicationDbContext context, ILogger<InvoicesController> looger)
        {
            _context = context;
            _logger = looger;
        }

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> GetDetailByInvoiceId(int id)
        {
            return await _context.InvoiceDetails.Where((ivd)=> ivd.InvoiceId == id)
                .OrderByDescending(ivd => ivd.Total)
                .ToListAsync();
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

        [HttpPut("concurrency-by-row")]
        public async Task<ActionResult> Put()
        {
            var invoiceId = 2; // Assuming we are updating the invoice with Id 2

            var invoice = await _context.Invoices.AsTracking().FirstOrDefaultAsync(i => i.Id == invoiceId);

            // Simulate a concurrency conflict by modifying the Version property
            invoice.TransactionDate = DateTime.Now;

            var version1 = invoice.Version;

            // Uncomment the next line to simulate a concurrency conflict
            await _context.Database
                .ExecuteSqlInterpolatedAsync(
                        $"UPDATE Invoices SET TransactionDate = GetDate() WHERE Id = {invoiceId}"
                    );


            await _context.SaveChangesAsync();

            return Ok(version1);
        }

        [HttpPut("concurrency-by-row-with-handle-errors")]
        public async Task<ActionResult> PutV2()
        {
            var invoiceId = 2; // Assuming we are updating the invoice with Id 2

            try
            {

                var invoice = await _context.Invoices
                                            .AsTracking()
                                            .FirstOrDefaultAsync(i => i.Id == invoiceId);

                // Simulate a concurrency conflict by modifying the Version property
                invoice.TransactionDate = DateTime.Now.AddDays(-10);

                var version1 = invoice.Version;

                // Uncomment the next line to simulate a concurrency conflict
                await _context.Database
                    .ExecuteSqlInterpolatedAsync(
                            $"UPDATE Invoices SET TransactionDate = GetDate() WHERE Id = {invoiceId}"
                        );


                await _context.SaveChangesAsync();

                return Ok(version1);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var currentInvoice = await _context.Invoices
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(i => i.Id == invoiceId);
                foreach(var property in entry.Metadata.GetProperties())
                {
                    var tryValue = entry.Property(property.Name).CurrentValue;
                    var currentValue = _context.Entry(currentInvoice).Property(property.Name).CurrentValue;
                    var oldValue = entry.Property(property.Name).OriginalValue;

                    if(currentValue.ToString() == tryValue.ToString())
                    {
                        continue; // No conflict, skip this property
                    }

                    _logger.LogInformation($"--- Property: {property.Name}, " +
                        $"Old Value: {oldValue}, " +
                        $"Current Value: {currentValue}, " +
                        $"Attempted Value: {tryValue}");
                }

                return BadRequest("Concurrency conflict detected. " +
                    "The invoice has been modified by another user. " +
                    "Please reload the invoice and try again.");
            }

        }

        //[HttpPut("concurrency-with-desconnected-model")]
        //public async Task<ActionResult<Invoice>> PutWithDesconnectedModel()
        //{
        //    //Para este caso, es requerido crear un modelo DTO que contenga los campos que se desean actualizar
        //    // y el valor original de ese campo para poder detectar conflictos de concurrencia.
        //    // por ejemplo, con el modelo genre o genero, al querer actualizar el nombre de un genero existente
        //    // se debe enviar el nombre original del genero y decirle a EF Core que campo ha sido el modificado
        //    // y cual era el valor original del campo.
        //    // asi, EF Core se encarga de actualizar el campo version y detectar conflictos de concurrencia.
        //}
    }
}
