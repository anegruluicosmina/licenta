using licenta.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Trebuie sa adaugi o cerinta pentru aceasta intrebare.")]
        [StringLength(255)]
        public string Text { get; set; }

        [Required(ErrorMessage ="Trebuie sa adaugi o explicatie")]
        [StringLength(500)]
        public string Explanation { get; set; }

        [StringLength(500)]
        public string  ImagePath { get; set; }        

        [NotMapped]
        public IFormFile Image { get; set; }

        public ICollection<Category> Category { get; set; }

        [Required(ErrorMessage = "Trebuie sa alegi o categorie pentru aceasta intrebare.")]
        public int CategoryId { get; set; }

        [MinOneCorrectAnswer(ErrorMessage ="Trebuie sa alegi macar un raspuns corect.")]
        public ICollection<Answer> Answers { get; set; }
    }
}
