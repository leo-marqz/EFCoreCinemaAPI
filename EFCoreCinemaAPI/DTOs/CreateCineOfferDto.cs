using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCinemaAPI.DTOs
{
    public class CreateCineOfferDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal DiscountPercentage { get; set; } //64.7%
    }
}
