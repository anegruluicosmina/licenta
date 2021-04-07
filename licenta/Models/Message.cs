using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string SenderUsername { get; set; }

        public string ReceiverUsername { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Mesaj")]
        public string  Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date{ get; set; }
        public bool IsSeen { get; set; } = false;

    }
}
