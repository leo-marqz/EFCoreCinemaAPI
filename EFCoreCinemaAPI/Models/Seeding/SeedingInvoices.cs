using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models.Seeding
{
    public static class SeedingInvoices
    {
        public static void Seed(ModelBuilder builder)
        {
            var invoice = new Invoice
            {
                Id = 3,
                TransactionDate = DateTime.Now
            };

            var items = new List<InvoiceDetail>()
            {
                new InvoiceDetail
                {
                    Id = 3,
                    InvoiceId = 3,
                    Product = "Product C",
                    Price = 15
                },
                new InvoiceDetail
                {
                    Id = 4,
                    InvoiceId = 3,
                    Product = "Product D",
                    Price = 25
                }
            };

            builder.Entity<Invoice>().HasData(invoice);
            builder.Entity<InvoiceDetail>().HasData(items);
        }
    }
}
