using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime StartHour { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndHour { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
