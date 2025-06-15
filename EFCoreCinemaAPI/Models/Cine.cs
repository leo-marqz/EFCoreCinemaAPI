
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
        public CineOffer CineOffer { get; set; }
        public List<CineRoom> CineRooms { get; set; }

        //campos de cine pero representados por otra entidad
        public CineProfile CineProfile { get; set; }

        public Address Address { get; set; }
    }
}
