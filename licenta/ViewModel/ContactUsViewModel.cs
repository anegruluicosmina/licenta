using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage ="Acest camp trebuie completat")]
        [EmailAddress(ErrorMessage ="Nu ai introds o adresa de email valida")]
        [Display(Name ="Adresa de email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Acest camp trebuie completat")]
        [StringLength(500)]
        [Display(Name ="Mesaj")]
        public string Message { get; set; }
    }
}
