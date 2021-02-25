using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int CategoryId { get; set; }
        public int NumberOfCorectAnswer { get; set; } = 0;
        public int NumberOfQuestions { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
