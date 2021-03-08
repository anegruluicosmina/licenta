using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name ="Nume")]
        [Required(ErrorMessage = "Introduceți numele dvs de familie.")]
        public string LastName { get; set; }


        [Display(Name = "Prenume")]
        [Required(ErrorMessage = "Introduceți prenumele dvs.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage ="Introduceți un email.")]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name ="Numărul de telefon")]
        [Required(ErrorMessage ="Introduceți numărul de telefon.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name ="Data nașterii")]
        [Required(ErrorMessage ="Introduceți data nașterii.")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name ="Parola")]
        [Required(ErrorMessage ="Introduceți o parola.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmarea parolei")]
        [Required(ErrorMessage = "Introduceți confirmarea parolei.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Parola și confirmarea parolei nu se potrivesc.")]
        public string ConfirmPassword { get; set; }

    }
}
