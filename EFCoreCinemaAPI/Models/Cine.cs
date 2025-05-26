
using NetTopologySuite.Geometries;

namespace EFCoreCinemaAPI.Models
{
    public class Cine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } //[Precision(precision: 9, scale: 2)]
        public Point Location { get; set; }
    }
}
