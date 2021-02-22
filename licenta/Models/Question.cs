using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public ICollection<Category> Category { get; set; }
        public int CategoryId { get; set; }


        public ICollection<Answer> Answers { get; set; }
    }
}
