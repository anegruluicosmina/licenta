using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Test
    {
        public int Id { get; set; }
        public int NumberOfCorrectAnswers { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public bool Passed { get; set; }
        public Test()
        {
            Id = 0;
        }
    }
}
