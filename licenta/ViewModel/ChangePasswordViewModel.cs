using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="Adăugați parola curentă")]
        [Display(Name ="Parola curentă")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage ="Adaugați parola nouă.")]
        [Display(Name = "Parola nouă")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage ="Trebuie să adăugați confirmarea parolei")]
        [Display(Name = "Confirmarea parolei noi")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Parola confrimată nu se potrivește cu parola nouă")]
        public string ConfirmNewPassword { get; set; }
    }
}
