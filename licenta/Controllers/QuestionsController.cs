using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using licenta.Data;
using licenta.Models;
using licenta.ViewModel;

namespace licenta.Controllers
{
    public class QuestionsController:Controller
    {
        private ApplicationDbContext _context;
        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // return the questions's categories
        public IActionResult Categories()
        {
            // var data = _context.Questions.Include(q => q.Difficulty).Include(q => q.Answers).OrderBy(r => Guid.NewGuid()).Take(5);
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Questions(int id)
        {
            var questionsInDb = _context.Question
                            .Include(c => c.Category).Where(c => c.CategoryId == id)
                            .Include(q => q.Answers)
                            .ToList();
            return View(questionsInDb);
        }
        public IActionResult Edit(int id)
        {
            var questionInDb = _context.Question.SingleOrDefault(q => q.Id == id);

            if (questionInDb == null)
                return StatusCode(404);

            var viewModel = new QuestionFormViewModel
            {
                Id = questionInDb.Id,
                Text = questionInDb.Text,
                CategoryId = questionInDb.CategoryId,
                Answers = _context.Answers.Where(a => a.QuestionId == id).ToList(),
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }
        public IActionResult Save(Question viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View("Edit",
                    new QuestionFormViewModel
                    {
                        Text = viewModel.Text,
                        Categories = _context.Categories.ToList(),
                        Answers = viewModel.Answers.ToList()
                    });
            }
            //ad custom anotation
            bool corrextAnswers = false;
            //errors that come from model binding and model validation
            foreach (var answer in viewModel.Answers)
            {
                if (answer.IsCorrect)
                    corrextAnswers = true;
            }

            if (!corrextAnswers)
                return Content("no correct answer");

            if (viewModel.Id == 0)
            {
                _context.Question.Add(viewModel);
                foreach (var answer in viewModel.Answers)
                {
                    _context.Answers.Add(answer);
                }
            }
            else
            {
                var questionInDb = _context.Question.Include(q => q.Answers).Single(q => q.Id == viewModel.Id);
                questionInDb.Text = viewModel.Text;
                questionInDb.Category = viewModel.Category;
                questionInDb.Answers = viewModel.Answers.Select(x => new Answer()
                {
                    // variable mapping here 
                    IsCorrect = x.IsCorrect,
                    Text = x.Text
                }).ToList();
                questionInDb.CategoryId = viewModel.CategoryId;
            }

            _context.SaveChanges();
            return RedirectToAction("Categories", "Questions");
        }

        public IActionResult New()
        {
            var categories = _context.Categories.ToList();
            QuestionFormViewModel viewModel = new QuestionFormViewModel()
            {
                Categories = categories
            };

            return View("Edit", viewModel);
        }

        public IActionResult Test(int id)
        {
            var numberOfQuestions = _context.Categories
                            .First(c => c.Id == id)
                            .NumberOfQuestions;

            var questionsInDb = _context.Question.Where(q => q.CategoryId == id)
                .Include(q => q.Answers)
                .OrderBy(r => Guid.NewGuid()).Take(numberOfQuestions)
                .ToList();


            TestViewModel viewModel = new TestViewModel()
            {
                Id = 1,
                Index = 0,
                Questions = new List<QuestionViewModel>(),
                NumberOfQuestions = numberOfQuestions,
                CategoryId = id
            };

            foreach (var question in questionsInDb)
            {
                QuestionViewModel questionModel = new QuestionViewModel
                {
                    Id = question.Id,
                    Text = question.Text,
                    Answers = question.Answers.Select(x => new AnswerViewModel()
                    {
                        // variable mapping here 
                        Id = x.Id,
                        Text = x.Text
                    }).ToList(),
                    Explanation = question.Explination
                };
                viewModel.Questions.Add(questionModel);                
            }

            /*var questions = _mapper.Map<List<Question>, List<QuestionViewModel>>(questionsInDb);*/

            return View(viewModel);
        }
        public IActionResult VerifyAnswer(string data, int questionid)
        {
            if (data == "[]" || questionid == 0)
                return Json(new { message = "Nu ati selectat un rapuns", id = 1 });

            var givenAnswers = JsonConvert.DeserializeObject<List<int>>(data);

            List<int> corectAnswers = _context.Answers
                .Where(a => a.QuestionId == questionid)
                .Where(a => a.IsCorrect == true)
                .Select(a => a.Id)
                .ToList();

            if (corectAnswers == null)
                return Json(new { message = "Eroare de server", id = "3" });

            /*            MVC defaults to DenyGet to protect you against a very specific attack involving JSON 
                            requests to improve the liklihood that the implications of allowing HTTP GET exposure 
                            are considered in advance of allowing them to occur.*/
            if (Enumerable.SequenceEqual(givenAnswers, corectAnswers))
                return Json(new { message = "Raspuns corect", id = "0" });

            return Json(new { message = "Raspuns gresit", id = "2" });

        }
        public IActionResult SaveAnswers(TestViewModel question)
        {
            bool ok = true;

            foreach (var answer in question.Questions[question.Index].Answers)
            {
                var isCorrect = _context.Answers
                    .SingleOrDefault(a => a.Id == answer.Id);

                if (isCorrect == null)
                    return StatusCode(404);

                if (answer.IsChecked != isCorrect.IsCorrect)
                    ok = false;
            }
            if (ok)
            {

            }
            return View("");
        }

        public IActionResult SaveTest(string correctAnswers, string categoryId)
        {
            Test test = new Test
            {
                NumberOfCorrectAnswers = Int32.Parse(correctAnswers),
                CategoryId = Int32.Parse(categoryId)
            };
            _context.Tests.Add(test);
            _context.SaveChanges();
            return Json("succes");
        }

        public IActionResult WrongAnswered (string data)
        {
            List<int> wrongAnswered = JsonConvert.DeserializeObject<List<int>>(data);

            var questionsInDb = _context.Question
                .Where(q => wrongAnswered.Contains(q.Id))
                .Include(q => q.Answers)
                .ToList();

            return View(questionsInDb);
        }

    }
}
