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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace licenta.Controllers
{
    public class QuestionsController:Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public QuestionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // return the categories
        public async Task<IActionResult> Categories(int id)
        {
            if (id == 0)
            {                
                var categoriesTest = await _context.Categories.ToListAsync();
                return View("TestCategories", categoriesTest);
            }
            var categoriesEdit = await _context.Categories.ToListAsync();
            return View("EditCategories", categoriesEdit);

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
        [Authorize(Roles ="Admin, Instructor")]
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

        [Authorize(Roles ="Admin, Instructor")]
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
        
        [Authorize(Roles ="Admin, Instructor")]
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

        [Authorize(Roles ="Admin, Instructor")]
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

        [Authorize(Roles ="Admin, Instructor")]
        //return view to add new category
        public async Task<IActionResult> NewCategory()
        {
            CategoryFormViewModel viewModel = new CategoryFormViewModel();
            return View("CategoryForm", viewModel);
        }

        [Authorize(Roles ="Admin, Instructor")]
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

        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteQuestion (int id)
        {
            var questionInDb =await  _context.Question
                                        .Include(q => q.Answers)
                                        .SingleOrDefaultAsync(q => q.Id == id);

            _context.Answers.RemoveRange(questionInDb.Answers);
            _context.Question.Remove(questionInDb);

            _context.SaveChanges();

            return RedirectToAction("Categories", new { id = 1 });
        }

        [Authorize(Roles ="Admin, Instructor")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);

            var questions = _context.Question.Where(q => q.CategoryId == id).Include(q => q.Answers).ToList();
            foreach (var question in questions)
                _context.Answers.RemoveRange(question.Answers);

           _context.Question.RemoveRange(questions);

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

            if(questionsInDb.Count == numberOfQuestions)
            {
                var user = await _userManager.GetUserAsync(User);

                TestViewModel viewModel = new TestViewModel()
                {
                    Id = 1,
                    NumberOfQuestions = questionsInDb.Count(),
                    Questions = new List<QuestionViewModel>(),
                    NumberOfWrongAnswer = await _context.Categories
                                                .Where(c => c.Id == id)
                                                .Select(c => c.NumberOfWrongQuestions)
                                                .SingleOrDefaultAsync(),
                    CategoryId = id,
                    UserId = user.Id
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
            else
            {
                return Content("Error");
            }
           
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
        public async Task<IActionResult> SaveTest(string correctAnswers, string categoryId, string userId)
        {
            var categoryPassed = _context.Categories.Where(c => c.Id == Int32.Parse(categoryId)).FirstOrDefault();

            Test test = new Test
            {
                NumberOfCorrectAnswers = Int32.Parse(correctAnswers),
                CategoryId = Int32.Parse(categoryId),
                UserId = userId,
                Date = DateTime.Now
            };
            if (Int32.Parse(correctAnswers) >= (categoryPassed.NumberOfQuestions - categoryPassed.NumberOfWrongQuestions))
            {
                test.Passed = true;
            }
            else
            {
                test.Passed = false;
            }
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
