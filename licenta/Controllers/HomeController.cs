using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using licenta.Models;
using licenta.ViewModel;
using licenta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace licenta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext contex;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext contex, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.contex = contex;
            this.userManager = userManager;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Message(string title, string text)
        {
            ViewBag.MessageTitle = title;
            ViewBag.MessageText = text;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProgramsAndRates()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> Instructors()
        {
            var users = await contex.Users.ToListAsync();
            var instructors = new List<ApplicationUser>();
            

            foreach(var user in users)
            {
                if(await userManager.IsInRoleAsync(user, "Instructor")){
                    instructors.Add(user);
                }
            }

            return View(instructors);
        }
    }
}
