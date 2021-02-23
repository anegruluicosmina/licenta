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

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int NumberOfQuestions { get; set; }
        public ICollection<Question> Question { get; set; }
    }
}
