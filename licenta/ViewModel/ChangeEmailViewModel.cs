using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class ChangeEmailViewModel
    {
        [Required(ErrorMessage = "Trebuie inserata o adresa de email.")]
        [EmailAddress(ErrorMessage ="Trebuie inserata o adresa de email.")]
        [Display(Name ="Introduceti noua adresa de email.")]
        public string Email { get; set; }
    }
}
