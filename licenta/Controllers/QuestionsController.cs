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
        // return the categories
        public async Task<IActionResult> Categories(int id)
        {

            var categories = await _context.Categories.ToListAsync();
            if(id == 0) 
                return View("TestCategories", categories);
            return View("EditCategories", categories);

        }
        //return the questions of a category
        public async Task<IActionResult> Questions(int id)
        {
            var questionsInDb = await _context.Question
                            .Include(c => c.Category).Where(c => c.CategoryId == id)
                            .Include(q => q.Answers)
                            .ToListAsync();
            return View(questionsInDb);
        }

        //edit question
        public async Task<IActionResult> Edit(int id)
        {
            var questionInDb = await _context.Question.SingleOrDefaultAsync(q => q.Id == id);

            if (questionInDb == null)
                return StatusCode(404);

            var viewModel = new QuestionFormViewModel
            {
                Id = questionInDb.Id,
                Text = questionInDb.Text,
                CategoryId = questionInDb.CategoryId,
                Explanation = questionInDb.Explanation,
                Answers = await _context.Answers.Where(a => a.QuestionId == id).ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            return View(viewModel);
        }

        //save the new or updated question
        public async Task<IActionResult> Save(Question viewModel)
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
            
            if (viewModel.Id == 0)
            {
                await _context.Question.AddAsync(viewModel);
                foreach (var answer in viewModel.Answers)
                {
                    await _context.Answers.AddAsync(answer);
                }
            }
            else
            {
                var questionInDb = await _context.Question
                    .Include(q => q.Answers)
                    .SingleAsync(q => q.Id == viewModel.Id);

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

            await _context.SaveChangesAsync();
            return RedirectToAction("Categories", "Questions", new { id = 1});
        }
        
        // return view to add new question
        public async Task<IActionResult> New()
        {
            var categories = await _context.Categories.ToListAsync();
            QuestionFormViewModel viewModel = new QuestionFormViewModel()
            {
                Categories = categories
            };

            return View("Edit", viewModel);
        }

        //save the new or updated category
        public async Task<IActionResult> SaveCategory(Category viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View("CategoryForm",
                    new CategoryFormViewModel
                    {
                        Name = viewModel.Name,
                        NumberOfQuestions = viewModel.NumberOfQuestions,
                        NumberOfWrongQuestions = viewModel.NumberOfWrongQuestions
                    });
            }

            if (viewModel.Id == 0)
            {
                await _context.Categories.AddAsync(viewModel);
            }
            else
            {
                var categoryInDb = await _context.Categories.SingleAsync(q => q.Id == viewModel.Id);

                categoryInDb.Name = viewModel.Name;
                categoryInDb.NumberOfQuestions = viewModel.NumberOfQuestions;
                categoryInDb.NumberOfWrongQuestions = viewModel.NumberOfWrongQuestions;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Categories", "Questions", new { id = 1 });
        }

        //return view to add new category
        public async Task<IActionResult> NewCategory()
        {
            CategoryFormViewModel viewModel = new CategoryFormViewModel();
            return View("CategoryForm", viewModel);
        }

        //return view to edit a category
        public async Task<IActionResult> EditCategoty(int id)
        {
            var categoryInDb = await _context.Categories.SingleOrDefaultAsync(q => q.Id == id);

            if (categoryInDb == null)
                return StatusCode(404);

            var viewModel = new CategoryFormViewModel
            {
                Name = categoryInDb.Name,
                NumberOfWrongQuestions = categoryInDb.NumberOfWrongQuestions,
                NumberOfQuestions = categoryInDb.NumberOfQuestions,
            };
            return View("CategoryForm", viewModel);
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Include(c => c.Question).Where(c => c.Id == id).SingleOrDefault();

            _context.Categories.Remove(category);
            var res = _context.SaveChanges();

            return RedirectToAction("Categories", new { id = 1});
        }

         //return view and data o a test
        public async Task<IActionResult> Test(int id)
        {
            var numberOfQuestions = _context.Categories
                            .First(c => c.Id == id)
                            .NumberOfQuestions;

            var questionsInDb = await _context.Question.Where(q => q.CategoryId == id)
                .Include(q => q.Answers)
                .OrderBy(r => Guid.NewGuid()).Take(numberOfQuestions)
                .ToListAsync();


            TestViewModel viewModel = new TestViewModel()
            {
                Id = 1,
                NumberOfQuestions = numberOfQuestions,
                Questions = new List<QuestionViewModel>(),
                NumberOfWrongAnswer =await _context.Categories
                                            .Where(c => c.Id == id)
                                            .Select(c => c.NumberOfWrongQuestions)
                                            .SingleOrDefaultAsync(),
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
                    Explanation = question.Explanation
                };
                viewModel.Questions.Add(questionModel);                
            }

            /*var questions = _mapper.Map<List<Question>, List<QuestionViewModel>>(questionsInDb);*/

            return View(viewModel);
        }

        //check the correctness of an answer
        public async Task<IActionResult> VerifyAnswer(string data, int questionid)
        {
            if (data == "[]" || questionid == 0)
                return Json(new { message = "Nu ati selectat un rapuns", id = 1 });

            var givenAnswers = JsonConvert.DeserializeObject<List<int>>(data);

            List<int> corectAnswers =await _context.Answers
                .Where(a => a.QuestionId == questionid)
                .Where(a => a.IsCorrect == true)
                .Select(a => a.Id)
                .ToListAsync();

            if (corectAnswers == null)
                return Json(new { message = "Eroare de server", id = "3" });

            /*            MVC defaults to DenyGet to protect you against a very specific attack involving JSON 
                            requests to improve the liklihood that the implications of allowing HTTP GET exposure 
                            are considered in advance of allowing them to occur.*/
            if (Enumerable.SequenceEqual(givenAnswers, corectAnswers))
                return Json(new { message = "Raspuns corect", id = "0" });

            return Json(new { message = "Raspuns gresit", id = "2" });

        }

        //save result of test
        public async Task<IActionResult> SaveTest(string correctAnswers, string categoryId)
        {
            Test test = new Test
            {
                NumberOfCorrectAnswers = Int32.Parse(correctAnswers),
                CategoryId = Int32.Parse(categoryId)
            };
            await _context.Tests.AddAsync(test);
            await _context.SaveChangesAsync();
            return Json("succes");
        }

        //show to wronged answered questions when test is finished
        public async Task<IActionResult> WrongAnswered (string data)
        {
            List<int> wrongAnswered = JsonConvert.DeserializeObject<List<int>>(data);

            var questionsInDb =await  _context.Question
                .Where(q => wrongAnswered.Contains(q.Id))
                .Include(q => q.Answers)
                .ToListAsync();

            return View(questionsInDb);
        }
    }
}
