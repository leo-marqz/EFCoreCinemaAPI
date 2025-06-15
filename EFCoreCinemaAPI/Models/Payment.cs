using System;

namespace EFCoreCinemaAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public PaymentType Type { get; set; }
    }
}
