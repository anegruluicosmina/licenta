using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class Test
    {
        public int Id { get; set; }
        public ICollection<Question> Questions { get; set; }
        public int NumberOfCorrectAnswers { get; set; }

        public int NumberOfQuestions { get; set; }

        private readonly Random random = new Random();
        public Test()
        {
            Id = 0;
        }
    }
}
