using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name ="Numele de utilizator")]
        [Required(ErrorMessage ="Trebuie sa introduceti email-ul")]
        [EmailAddress(ErrorMessage = "Adresa introdusă de email nu este validă")]
        public string Username { get; set; }

        [Display(Name ="Parola")]
        [Required(ErrorMessage ="Trebuie sa va introduceti parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Pastreaza-ma conectat")]
        public bool RememberMe { get; set; }
    }
}
