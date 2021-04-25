using licenta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Categoria are nevoie de un nume.")]
        [Display(Name ="Numele categoriei")]
        public string Name { get; set; }

        [Display(Name="Numărul de întrebări dintr-un test")]
        [Required(ErrorMessage ="Acest câmp trebuie completat")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "Introdu un număr")]
        [Range(3, 1000, ErrorMessage = "Valoarea trebuie sa fie de minim 3")]
        public int NumberOfQuestions { get; set; }

        [Display(Name ="Numărul de întrebări la care se poate greși într-un test")]
        [Required(ErrorMessage ="Acest câmp trebuie completat")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "Introdu un număr")]
        public int NumberOfWrongQuestions { get; set; }

        [Display(Name = "Timp(în minute)")]
        [Required(ErrorMessage ="Acest câmp trebuie completat")]
        public int Time { get; set; }

        public string Title
        {
            get
            {
                if (Id == 0)
                    return "Adăugare categorie nouă";
                return "Editare întrebare";
            }
        }
        public CategoryFormViewModel()
        {
            Id = 0;
        }
    }
}
