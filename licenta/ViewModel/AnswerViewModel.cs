using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        public bool IsChecked { get; set; }

        public bool IsCorrect { get; set; }
    }
}
