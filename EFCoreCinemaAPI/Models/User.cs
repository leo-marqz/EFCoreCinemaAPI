using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCinemaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //para inverse property
        [InverseProperty("Emitter")] //propiedad de clase Message
        public List<Message> SentMessages { get; set; }

        [InverseProperty("Receiver")] //propiedad de clase Message
        public List<Message> ReceivedMessages { get; set; }
    }
}
