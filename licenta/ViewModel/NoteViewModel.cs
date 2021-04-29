using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class NoteViewModel
    {
        [Display(Name ="Data")]
        [Required(ErrorMessage = "Trebuie să introduceți o valoare pentru acest câmp")]
        [DataType(DataType.Date)]
        public DateTime Date{ get; set; }

        [Display(Name ="Ora de început")]
        [Required(ErrorMessage = "Trebuie să introduceți o valoare pentru acest câmp")]
        [DataType(DataType.Time)]
        public DateTime StartTime{ get; set; }

        [Display(Name ="Ora de sfârșit")]
        [Required(ErrorMessage = "Trebuie să introduceți o valoare pentru acest câmp")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Display(Name ="Descriere")]
        [Required(ErrorMessage = "Trebuie să introduceți o valoare pentru acest câmp")]
        [StringLength(500, ErrorMessage ="Prea multe caractere introduse")]
        public string Description { get; set; }
    }
}
