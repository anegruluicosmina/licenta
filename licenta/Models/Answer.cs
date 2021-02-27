using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Trebuie sa introduci un text pentru raspuns.")]
        [StringLength(255)]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public Question Questions { get; set; }
    }
}
