using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using licenta.Data;
using licenta.Models;
using licenta.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace licenta.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManage;

        public NotesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManage = userManager;
        }



        //returns all notes in one day 

        public async Task<IActionResult> Notes(DateTime date)
        {
            var user = await _userManage.GetUserAsync(User);
            if (user == null)
                return StatusCode(400);

            var defaultDate = new DateTime();

            if (date == defaultDate)
            {
                date = DateTime.Now;
            }
            ViewBag.Date = date.Date;
            ViewBag.PreviousDate = date.Date.AddDays(-1);
            ViewBag.NextDate = date.Date.AddDays(1);
            var note = await _context.Notes.Where(n => (n.UserId == user.Id && DateTime.Compare(n.Date.Date, date.Date) == 0))
                .OrderBy(n => n.StartHour)
                .ToListAsync();
            return View(note);
        }




        // add new note
        public async Task<IActionResult> NewNote(DateTime date)
        {
            NoteViewModel viewModel = new NoteViewModel();

            if (date != new DateTime())
                viewModel.Date = date;
            else
                viewModel.Date = DateTime.Now.Date;

            if (DateTime.Compare(date.Date, DateTime.Now.Date) == 0)
                viewModel.StartTime = DateTime.Now;

            return View("NoteForm", viewModel);
        }



        //return view for edit existing note
        [HttpGet]
        public async Task<IActionResult> EditNote(int id)
        {
            var note = _context.Notes.Where(n => n.Id == id).FirstOrDefault();
            var viewModel = new NoteViewModel
            {
                Date = note.Date,
                StartTime= note.StartHour,
                EndTime = note.EndHour,
                Description = note.Description
            };
            return View("NoteForm", viewModel);
        }


        //save the new note
        [HttpPost]
        public async Task<IActionResult> SaveNote(NoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("NoteForm", viewModel);
            }
            else
            {
                var user = await _userManage.GetUserAsync(User);
                //check if date is before the current date
                if (DateTime.Compare(viewModel.Date.Date, DateTime.Now.Date) < 0)
                {
                    ModelState.AddModelError("To early", "Nu se poate realiza o programare la o data anteriora");
                    return View("NoteForm", viewModel);
                }
                //check if hour from current date is before current moment
                if (DateTime.Compare(viewModel.Date.Date, DateTime.Now.Date) == 0 && TimeSpan.Compare(viewModel.StartTime.TimeOfDay, DateTime.Now.TimeOfDay) < 0)
                {
                    ModelState.AddModelError("IncorrectHours", "Nu puteți realiza o notă pentru o dată anterioară");
                    return View("NoteForm", viewModel);
                }
                //check if the start hour and end hour are valid
                if (TimeSpan.Compare(viewModel.EndTime.TimeOfDay, viewModel.StartTime.TimeOfDay) <= 0)
                {
                    ModelState.AddModelError("IncorrectHours", "Orele nu sunt valide");
                    return View("NoteForm", viewModel);
                }
                //check if current user has an appointment for that moment
                var noteInDb = _context.Notes.Where(n => (n.UserId == user.Id && DateTime.Compare(n.Date.Date, viewModel.Date.Date) == 0 && TimeSpan.Compare(n.StartHour.TimeOfDay, viewModel.StartTime.TimeOfDay) <= 0 && TimeSpan.Compare(n.EndHour.TimeOfDay, viewModel.EndTime.TimeOfDay)>=0)).ToList();
                
                if(noteInDb.Count() != 0)
                {
                    ModelState.AddModelError("Exists", "Exista deja o programare in baza de date in acest interval");
                    return View("NoteForm", viewModel);
                }
                
                var note = new Note
                {
                    Date = viewModel.Date,
                    StartHour = viewModel.StartTime,
                    EndHour = viewModel.EndTime,
                    Description = viewModel.Description,
                    UserId = user.Id
                };
                await _context.Notes.AddAsync(note);
                _context.SaveChanges();
                return RedirectToAction("Notes", viewModel.Date);
            }
        }



        //delete a note
        public IActionResult DeleteNote(int id)
        {
            if (id == null)
                return StatusCode(404);

            var noteInDb = _context.Notes.Where(n => n.Id == id).FirstOrDefault();
            _context.Notes.Remove(noteInDb);
            _context.SaveChanges();
            return Json("successful deletion");
        }
    }
}
