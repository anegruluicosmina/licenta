using licenta.Models;
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

        [MinOneCorrectAnswer]
        public List<Answer> Answers { get; set; }

        [Required(ErrorMessage = "bbbbb")]
        public List<Category> Categories { get; set; }

        [Required(ErrorMessage = "Please select a project owner")]
        [StringLength(255)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public int NumberOfAnswers { get; set; } = 3;

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
