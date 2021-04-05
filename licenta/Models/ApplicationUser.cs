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
        [StringLength(255, ErrorMessage ="Introduceti o valoarea de lungime mai scurta.")]
        public string FirstName { get; set; }

        [Display(Name = "Nume")]
        [Required(ErrorMessage = "Introduceți numele dvs de familie.")]
        [StringLength(255, ErrorMessage = "Introduceti o valoarea de lungime mai scurta.")]
        public string LastName { get; set; }

        [Display(Name = "Data nașterii")]
        [Required(ErrorMessage = "Introduceți data nașterii.")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Descriere")]
        [StringLength(500, ErrorMessage = "Introduceti o valoarea de lungime mai scurta.")]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Varsta")]
        public int Age { get; set; }
        public List<Test> Tests { get; set; }
        public ICollection<Message> Messages{ get; set; }

        public ICollection<Connection> Connections { get; set; }
    }
}
