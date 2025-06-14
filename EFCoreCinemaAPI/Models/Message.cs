using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCinemaAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }

        //[ForeignKey(nameof(EmitterId))]
        public int EmitterId { get; set; }
        public User Emitter { get; set; }

        //[ForeignKey(nameof(ReceiverId))]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
