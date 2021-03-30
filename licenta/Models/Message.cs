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
        public string UserId { get; set; }

        //just if it is not connected, if not is optional[Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Mesaj")]
        public string  Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date{ get; set; }

    }
}
