using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Categoria are nevoie de un nume")]
        [StringLength(10)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trebuie sa adaugi care este numar de intrebari intr-un test pentru aceasta categorie.")]
        public int NumberOfQuestions { get; set; }

        [NrOfWrongAnsweresQst]
        [Required (ErrorMessage = "Trebuie sa adaugi care este numarul minim de intrebari care pot fi gesite la un test pentru aceasta categorie.")]
        public int NumberOfWrongQuestions { get; set; }

        [Required]
        public int Time { get; set; }

        public ICollection<Question> Question { get; set; }
    }
}
