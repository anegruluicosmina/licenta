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
                            .OrderBy(r => Guid.NewGuid()).Take(5)
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
    }
}
