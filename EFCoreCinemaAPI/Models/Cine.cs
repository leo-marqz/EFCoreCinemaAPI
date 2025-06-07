
using NetTopologySuite.Geometries;
using System.Collections.Generic;

namespace EFCoreCinemaAPI.Models
{
    public class Cine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }

        //Navigation property
        public virtual CineOffer CineOffer { get; set; }
        public virtual HashSet<CineRoom> CineRooms { get; set; }
    }
}
