using System.ComponentModel.DataAnnotations;

namespace EFCoreCinemaAPI.Models
{
    public class CineProfile
    {
        public int Id { get; set; }
        [Required]
        public string History { get; set; }
        public string CoreValues { get; set; }
        public string Mision { get; set; }
        public string Vision { get; set; }
        public string CodeOfEthics { get; set; }

        public Cine Cine { get; set; }
    }
}
