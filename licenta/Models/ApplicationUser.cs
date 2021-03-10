using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Prenume")]
        [Required(ErrorMessage = "Introduceți prenumele dvs.")]
        public string FirstName { get; set; }

        [Display(Name = "Nume")]
        [Required(ErrorMessage = "Introduceți numele dvs de familie.")]
        public string LastName { get; set; }

        [Display(Name = "Data nașterii")]
        [Required(ErrorMessage = "Introduceți data nașterii.")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
