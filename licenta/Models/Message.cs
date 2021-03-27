using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Message
    {
        public int Id { get; set; }

        //just if it is not connected, if not is optional[Required]
        [EmailAddress]
        public string Form { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name="Subiect")]
        public string  Subject { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Mesaj")]
        public string  Body { get; set; }

        public MessageCategory Category { get; set; }
    }
}
