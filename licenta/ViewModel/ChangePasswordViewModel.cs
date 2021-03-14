using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="Adaugati parola curenta.")]
        [Display(Name ="Parola curenta")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage ="Adaugati parola noua.")]
        [Display(Name = "Parola noua")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage ="Trebuie sa adaugati cconfirmarea parolei.")]
        [Display(Name = "Confirmarea parolei noi")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Parola comparata nu se potriveste cu parola noua.")]
        public string ConfirmNewPassword { get; set; }
    }
}
