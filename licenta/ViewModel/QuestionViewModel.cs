using licenta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public string Explanation { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}
