
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EFCoreCinemaAPI.Models
{
    public class Cine : Notification
    {
        private string _name;
        private Point _location;
        private CineOffer _offer;
        private List<CineRoom> _rooms;
        private CineProfile _profile;
        private Address _address;


        public int Id { get; set; }
        public string Name { get=> _name; set=> Set(value, ref _name); }
        public Point Location { get=>_location; set=>Set(value, ref _location); }

        //Navigation property
        public CineOffer CineOffer { get=>_offer; set=>Set(value, ref _offer); }

        //para esta propiedad se usa mejor observable y nos ahorramos hacer los pasos que 
        //hacemos con las otras propiedades
        public ObservableCollection<CineRoom> CineRooms { get; set; }

        //campos de cine pero representados por otra entidad
        public CineProfile CineProfile { get=>_profile; set=>Set(value, ref _profile); }

        public Address Address { get=>_address; set=>Set(value, ref _address); }
    }
}
