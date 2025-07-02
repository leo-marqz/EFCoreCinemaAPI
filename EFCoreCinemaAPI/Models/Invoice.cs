using System;

namespace EFCoreCinemaAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
