using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCinemaAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }

        //[Timestamp] // This attribute is used for concurrency checking
        public byte[] Version { get; set; }
    }
}
