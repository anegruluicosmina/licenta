using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nume")]
        [Required(ErrorMessage = "Introduceți numele dvs. de familie")]
        public string LastName { get; set; }


        [Display(Name = "Prenume")]
        [Required(ErrorMessage = "Introduceți prenumele dvs.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Introduceți un email.")]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "Numărul de telefon")]
        [Required(ErrorMessage = "Introduceți numărul de telefon.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Data nașterii")]
        [Required(ErrorMessage = "Introduceți data nașterii.")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Descriere")]
        [StringLength(500, ErrorMessage = "Introduceti o valoarea de lungime mai scurta.")]
        public string Description { get; set; }

    }
}
