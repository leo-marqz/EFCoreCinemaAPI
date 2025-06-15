using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCinemaAPI.Models
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
    }
}
