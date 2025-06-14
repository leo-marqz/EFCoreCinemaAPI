using System;

namespace EFCoreCinemaAPI.Models
{
    public class CineOffer
    {
        public int Id { get; set; }

        //marcando como nullable 
        //esto nos permite eliminar un cine sin eliminar la oferta
        //tambien se le puede llamar relacion opcional
        public int? CineId { get; set; } 
        public decimal DiscountPercentage { get; set; } //64.7%
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Cine Cine { get; set; }
    }
}
