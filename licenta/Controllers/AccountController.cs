using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using licenta.Data;
using licenta.Models;
using licenta.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;

namespace licenta.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _context = context;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            /*return View();*/
            return Content("AccessDenied");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                TimeSpan date = DateTime.Now - registerViewModel.Birthday;
                var age = DateTime.MinValue + date;
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    Birthday = registerViewModel.Birthday,
                    Age = age.Year

                };
               var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "Cursant");
                    if (!addRoleResult.Succeeded)
                    {
                        return Content("Sory there has been a probleme contact the page admin");
                    }
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await _emailService.SendAsync(user.Email, "verificare email", $"<a href = \"{confirmationLink}\">Confirma email</a>", true);

                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    /* session cookie*/
                    /*                    await _signInManager.SignInAsync(user, false);
                                        return RedirectToAction("index", "home");*/
                    ModelState.AddModelError("", "Inregistrare cu succes.");
                    ModelState.AddModelError("", "Inaite de a te autentifica, esti nevoit sa confirmi adresa de email printr-un click pe link-ul trimis pe email de catre noi.");
/*                    ViewBag.ErrorTitle = "Inregistrare cu succes.";
                    ViewBag.ErrorMessage = "Inaite de a te autentifica, esti nevoit sa confirmi adresa de email printr-un click pe link-ul trimis pe email de catre noi.";*/
                    return View(registerViewModel);
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Username);
                if (!(await _userManager.CheckPasswordAsync(user, viewModel.Password)) || user == null)
                {
                    ModelState.AddModelError("", "Email-ul sau parola sunt gresite.");
                    return View(viewModel);
                }
                if(user != null && user.EmailConfirmed == false)
                {
                    ModelState.AddModelError("", "Email-ul nu a fost inca confirmat");
                    return View(viewModel);
                }

                var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "home");
                }

                ModelState.AddModelError(string.Empty, "Autentificare esuata.");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProfileMenu(string submit_btn)
        {
            switch (submit_btn) {
                case "Schimba parola":
                    return RedirectToAction("ChangePassword");
                case "Editeaza profilul":
                    return RedirectToAction("EditUser");
                case "Schimba email":
                    return RedirectToAction("ChangeEmail");
                case "Testele mele":
                    return RedirectToAction("TestsResults");
                default:
                    return RedirectToAction("Profile");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if(userId == null)
            {
                return RedirectToAction("Login");
            }

            var user =await _userManager.FindByIdAsync(userId);
            var role = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Description = user.Description
            };

            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);

            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.PhoneNumber = viewModel.PhoneNumber;
                user.Description = viewModel.Description;
            }
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return View("Profile");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return Content("user  not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
/*add view for confirmed email*/
                return Content("Email confirmat");
            }
            return Content("email-ul nun a putut fi confirmat");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasssword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                if(user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, token = token }, Request.Scheme);

                    await _emailService.SendAsync(viewModel.Email, "Resetare parola", $"<a href = \"{passwordResetLink}\">Resetare parola</a>", true);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword (string token, string email)
        {
            if(token == null || email == null)
            {
                Content("Invalid reset password token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);

                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach( var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(viewModel);
                }
                return View("ResetPasswordConfirmation");
            }
         return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> TestsResults()
        {
            var user = await _userManager.GetUserAsync(User);
            var userTests = _context.Tests.Where(c => c.UserId == user.Id).Select(c => new { c.CategoryId, c.Category.Name, c.Date.Date }).ToList();
            var usercategories = userTests.Select(t => new { t.CategoryId, t.Name }).Distinct().ToList();
            var viewModel = new List<ProfileCategoriesViewModel>();
            foreach (var category in usercategories)
            {
                var count = userTests.Where(t => t.CategoryId == category.CategoryId).Select(t => t.Date.Date).Distinct().Count();
                if (count > 1)
                {
                    viewModel.Add(new ProfileCategoriesViewModel
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.Name,
                        IsLinear = true
                    });
                }
                else
                {
                    viewModel.Add(new ProfileCategoriesViewModel
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.Name,
                        IsLinear = false
                    });
                }

            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return RedirectToAction("Login");

                var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);
                return Content("ChangePwdConfirmation");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if(user == null)
                {
                    return RedirectToAction("Login");
                }
                user.UserName = viewModel.Email;
                user.Email = viewModel.Email;
                var resultUpdate = await _userManager.UpdateAsync(user);
                if (resultUpdate.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmChangeEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await _emailService.SendAsync(viewModel.Email, "verificare email", $"<a href = \"{confirmationLink}\">Confirma email</a>", true);

                    return Content("Un email a fost trimis la adresa adaugata");
                }
                else
                {
                    return Content("Update nereusit");
                }

            }
            return View(viewModel);
        }

        public async Task<IActionResult> ConfirmChangeEmail( string userId, string token)
        {
            if(userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            
            var user = await _userManager.FindByIdAsync(userId);
                        
            if (user == null)
            {
                return Content("user not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return Content("Email confirmat");     
            
            return Content("Confirmare nereusita");
        }

        public async Task<JsonResult> ProfileChart()
        {
            var user= await _userManager.GetUserAsync(User);
            int nrUserTests = _context.Tests.Where(t => t.UserId == user.Id).Count();
            int nrUserPassedTests = _context.Tests.Where(t => t.UserId == user.Id).Where(t => t.Passed == true).Count();


            BarChartDataSet dataset = new BarChartDataSet()
            {
                data = new int[] { nrUserPassedTests, nrUserTests-nrUserPassedTests},
                backgroundColor = new string[] {"#8FB258",
                                                "#E14B3B"
                },
                borderColor = new string[] {"#41924B",
                                            "#BF381A"
                },
                borderWidth = 1
            };

            PieChartData data = new PieChartData()
            {
                labels = new string[] { "Numarul de teste trecute", "Numarul de teste picate"},
                datasets = new BarChartDataSet[] { dataset }
            };

            return Json(data);
        }

        public async Task<IActionResult> ProfileChartCategory(int categoryId)
        {
            var user = await _userManager.GetUserAsync(User);
            var tests = _context.Tests.Where(t => t.UserId == user.Id).Where(t => t.CategoryId == categoryId).ToList();
            List<DateTime> dates = tests.Select(t => t.Date.Date).Distinct().ToList();

            List<string> dateList = new List<string>();
            foreach(var date in dates)
            {
                dateList.Add(date.Date.ToString("d/M/yyyy"));
            }
            string[] labels = dateList.ToArray();

            List<int> passedExamensList = new List<int>();
            List<int> examensList = new List<int>();

            LineChartDataSet lineChartDataSet = new LineChartDataSet();

            foreach(var date in dates)
            {
                passedExamensList.Add(tests.Where(c => c.Date.Date <= date).Where(c => c.Passed == true).Count());
                examensList.Add(tests.Where(c => c.Date.Date <= date).Count());

            }
            int[] passedExamens = passedExamensList.ConvertAll<int>(item => (int)item).ToArray();
            int[] examens = examensList.ConvertAll<int>(item => (int)item).ToArray();
            LineChartDataSet passedExamensData = new LineChartDataSet()
            {
                data = passedExamens,
                label = "Examene trecute",
                borderColor = "#41924B",
                backgroundColor = "#41924B",
                fill = false

            };
            LineChartDataSet examensData = new LineChartDataSet()
            {
                data = examens,
                label = "Numarul total de examene",                
                borderColor = "#7195A3",
                backgroundColor = "#7195A3",
                fill = false

            };
            /* = passedExamensList.ConvertAll<int>(item => (int)item).ToArray();*/
            LineChartData dataset = new LineChartData()
            {
                labels = labels,
                datasets = new LineChartDataSet[] { examensData , passedExamensData }

            };
            return Json(dataset);
        }
        public async Task<IActionResult> BarChartCategory(int categoryId)
        {
            var user = await _userManager.GetUserAsync(User);
            int nrUserTests = _context.Tests.Where(t => t.UserId == user.Id).Where(c => c.CategoryId == categoryId).Count();
            int nrUserPassedTests = _context.Tests.Where(t => t.UserId == user.Id).Where(t => t.Passed == true).Where(c => c.CategoryId == categoryId).Count();


            BarChartDataSet dataset = new BarChartDataSet()
            {
                data = new int[] { nrUserTests ,nrUserPassedTests },
                backgroundColor = new string[] {"#7195A3",
                                                "#8FB258"
                },
                borderColor = new string[] {"#7195A3",
                                                "#8FB258"
                },
                borderWidth = 2
            };

            BarChartData data = new BarChartData()
            {
                labels = new string[] { "Numarul de examene", "Numarul de teste trecute"  },
                datasets = new BarChartDataSet[] { dataset }
            };

            return Json(data);
        }
        public async Task<IActionResult> SendMessage(string email, string message)
        {
            if (email != null || message != null)
            {
                string subject = "Email from users without account";
                string emailBody = "Email adress: <b>" +email + "</b></br>" + message;
                await _emailService.SendAsync("drivingschool@yahoo.com",subject,emailBody, true);
                return Json(new { error_message = "Am primit mesajul tau. Echipa va reveni cu un raspuns cat de curand. Multumim!"});
            }
            return Json(new {error_message = "Nu ai completata toate campurile" });

        }
        public async Task<IActionResult> Chat()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = user.UserName;
            IEnumerable<Message> messages = null;
            return View(messages);
        }
        //save messages to database
        public async Task<IActionResult> SaveMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    message.UserName = user.UserName;
                    message.UserId = user.Id;
                    await _context.AddAsync(message);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

            }
            return StatusCode(400);
        }
    }
}
