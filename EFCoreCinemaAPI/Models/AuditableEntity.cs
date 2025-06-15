using System.ComponentModel.DataAnnotations;

namespace EFCoreCinemaAPI.Models
{
    public class AuditableEntity
    {
        [StringLength(150)]
        public string CreatedBy { get; set; }

        [StringLength(150)]
        public string ModifiedBy { get; set; }
    }
}
