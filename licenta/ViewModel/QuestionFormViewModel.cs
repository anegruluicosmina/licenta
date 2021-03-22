using licenta.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class QuestionFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduceti un text pentru intrebare")]
        [Display(Name ="Textul intrebarii")]
        public string Text { get; set; }

        [MinOneCorrectAnswer (ErrorMessage ="uauua")]
        public List<Answer> Answers { get; set; }

        public List<Category> Categories { get; set; }

        [Required(ErrorMessage = "Alege o categorie.")]
        [StringLength(255)]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }

        public int NumberOfAnswers { get; set; } = 3;

        [Display(Name = "Explicație")]
        [Required(ErrorMessage = "Introduceti o explicație pentru intrebare")]
        public string Explanation { get; set; }

        [Display(Name ="Imagine")]
        [DataType(DataType.Upload)]
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }

        public bool Saved { get; set; }

        //display the title for the page
        public string Title
        {
            get
            {
                if (Id == 0)
                    return "Adaugare intrebare noua";
                return "Editare intrebare";
            }
        }

        //to a default value for question when a new one it is created
        public QuestionFormViewModel()
        {
            Id = 0;
        }
    }
}
