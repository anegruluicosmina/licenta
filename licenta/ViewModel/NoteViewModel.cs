using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class NoteViewModel
    {
        [Display(Name ="data")]
        [Required(ErrorMessage ="Trebuie sa introduceti o valoeare pentru acest camp")]
        [DataType(DataType.Date)]
        public DateTime Date{ get; set; }

        [Display(Name ="Ora de inceput")]
        [Required(ErrorMessage = "Trebuie sa introduceti o valoeare pentru acest camp")]
        [DataType(DataType.Time)]
        public DateTime StartTime{ get; set; }

        [Display(Name ="Ora de sfarsit")]
        [Required(ErrorMessage = "Trebuie sa introduceti o valoeare pentru acest camp")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Display(Name ="Descriere")]
        [Required(ErrorMessage = "Trebuie sa introduceti o valoeare pentru acest camp")]
        [StringLength(500, ErrorMessage ="Prea multe caractere introduse")]
        public string Description { get; set; }
    }
}
