using System;

namespace EFCoreCinemaAPI.Models
{
    public class CineOffer
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; } //64.7%
        public int CineId { get; set; }
    }
}
