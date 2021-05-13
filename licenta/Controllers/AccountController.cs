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
using Microsoft.EntityFrameworkCore;
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



        //register user
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            //if the model state is valid
            if (ModelState.IsValid)
            {
                //calculate the birthday 
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
               //create user in db
               var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    //if the user was created successfully assigns the role of "CURSANT" 
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "Cursant");
                    if (!addRoleResult.Succeeded)
                    {
                        return Content("Sory there has been a probleme contact the page admin");
                    }
                    var text = "Pentru a vă putea confirma adresa de email da-ți click pe link-ul de mai jos. În cazul în care nu recunoașteți această cerere puteți ignora acest email.";
                    //create token and send email
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await _emailService.SendAsync(user.Email, "verificare email",text + $"</br>"+ $"<a href = \"{confirmationLink}\">Confirma email</a>", true);
                    //if the register has been by admin send him to the page with the list of users
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }


                    ViewBag.MessageTitle = "Înregistrare cu succes.";
                    ViewBag.MessageText = "Înaite de a te autentifica, ești nevoit să confirmi adresa de email printr-un click pe link-ul trimis pe email de către noi.";

                    return View("~/Views/Home/Message.cshtml");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerViewModel);
        }



        //return view for log in form
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        //login user by username and password
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //find user in db by username(email)
                var user = await _userManager.FindByEmailAsync(viewModel.Username);

                //check if password is correct
                if (!(await _userManager.CheckPasswordAsync(user, viewModel.Password)) || user == null)
                {
                    ModelState.AddModelError("", "Email-ul sau parola sunt greșite.");
                    return View(viewModel);
                }

                //check if the user cofirmed his email address
                if(user != null && user.EmailConfirmed == false)
                {
                    ModelState.AddModelError("", "Email-ul nu a fost înca confirmat");
                    return View(viewModel);
                }

                //sign in the user
                var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "home");
                }

                ModelState.AddModelError(string.Empty, "Autentificare eșuată.");
            }

            //if model is not valid return the view with the error messaged
            return View(viewModel);
        }

        //logout the user
        [Authorize]
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


        //return view for edit user
        [Authorize]
        [HttpGet]
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



        //edit user information
        [Authorize]
        [HttpPost]
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
                ModelState.AddModelError("", "Actualizările au fost salvate.");
                return View(viewModel);
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


        //confirm the email adress
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            //check that the required data has been sent 
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            //find user in db by id
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            { 
                ViewBag.MessageTitle = "Utilizatorul nu a fost gasit.";
                return View("~/Views/Home/Message.cshtml");
            }

            //check that the user confirmed email address
            var result = await _userManager.ConfirmEmailAsync(user, token);
            
            if (result.Succeeded)
            {
                ViewBag.MessageTitle = "Emailul este confirmat.";
                ViewBag.MessageText = "Acum te poți autentifica în aplicație.";
                return View("~/Views/Home/Message.cshtml");
            }

            ViewBag.MessageTitle = "Email-ul un a putut fi confirmat";
            ViewBag.MessageText = "Contacteză administratorii prin secțiunea 'Contactează-ne' de mai jos.";
            return View("~/Views/Home/Message.cshtml");
        }


        //return view to recover password if forgotten
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }



        //change password if forgotten
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);

                if (user == null)
                {
                    ViewBag.MessageTitle = "Nu a fost găsit un utilizator cu această adresă de email.";
                    ViewBag.MessageText = "Mai încearcă înca o dată și introdu cu atenție adresa de email.";
                    return View("~/Views/Home/Message.cshtml");

                }

                if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
                {
                    ViewBag.MessageTitle = "Emailul nu a fost confirmat.";
                    ViewBag.MessageText = "După ce te-ai înregistrat în aplicație ți-a fost trimis pe adresa de email un mesaj care conținea un link pentru a confirma adresa de email. " +
                        "Înainte de a reseta parola confirma adresa de emailul. " +
                        "În cazul în care nu ai primit mesajul contactează-ne.";
                    return View("~/Views/Home/Message.cshtml");

                }

                //recover password 
                if(user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, token = token }, Request.Scheme);

                    await _emailService.SendAsync(viewModel.Email, "Resetare parola", $"<a href = \"{passwordResetLink}\">Resetare parola</a>", true);

                    ViewBag.MessageTitle = "Am înregistrat cererea ta.";
                    ViewBag.MessageText = "Un email a fost trimis la adresa de email introdusă conținând un link care te trimite către o pagină unde îți poți schimba parola. " +
                        "În cazul în care nu primești mesajul contactează echipa cu ajutorul datelor de contacte de mai jos.";
                    return View("~/Views/Home/Message.cshtml");
                }
            }

            return View(viewModel);
        }



        //return view to reset de password using the token and email address
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword (string token, string email)
        {
            if(token == null || email == null)
            {
                ViewBag.MessageTitle = "Codul de autentificare nu mai este valid.";
                ViewBag.MessageText = "Pentru mai multe datilii contactează-ne folosind datele de contact de mai jos.";
                return View("~/Views/Home/Message.cshtml");
            }
            return View();
        }




        //reset password for the user using password and password confirmation
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
                        ViewBag.MessageTitle = "Parola a fost resetată cu succes.";
                        return View("~/Views/Home/Message.cshtml");
                    }
                    foreach( var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(viewModel);
                }
                ViewBag.MessageTitle = "Parola a fost resetată cu succes.";
                return View("~/Views/Home/Message.cshtml");
            }
            return View(viewModel);
        }


        //return view to change password for a logged user
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }



        //change password of the logged user
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                //if the user is not logged send him to login page
                if (user == null)
                    return RedirectToAction("Login");

                var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(viewModel);
                }

                await _signInManager.RefreshSignInAsync(user);
                ViewBag.MessageTitle = "Parola a fost schimbată cu succes.";
                return View("~/Views/Home/Message.cshtml");
            }
            return View(viewModel);
        }


        //return view for change email functionality
        [HttpGet]
        [Authorize]
        public IActionResult ChangeEmail()
        {
            return View();
        }


        //change email address of a logged user
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if(user == null)
                {
                    return RedirectToAction("Login");
                }

                if (user.Email.Equals(viewModel.Email))
                {
                    ModelState.AddModelError("", "Adresa pe care a-ți introdus-o este aceeași cu adresa de email a contului.");
                    return View(viewModel);
                }
                //replace data
                user.UserName = viewModel.Email;
                user.Email = viewModel.Email;
                user.EmailConfirmed = false;
                var resultUpdate = await _userManager.UpdateAsync(user);

                if (resultUpdate.Succeeded)
                {//send email and disconnect the user 
                    var emailText = "Adresa de email a fost schimbată cu aceasta. Pentru a te putea autentifica în cont trebuie să da-ți click pe link-ul de mai jos. Dacă nu a-ți solicitat acest lucru puteți să nu ignorați acest email.";
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmChangeEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await _emailService.SendAsync(viewModel.Email, "verificare email", emailText + $"</br>" + $"<a href = \"{confirmationLink}\">Confirmă email</a>", true);
                    
                    await _signInManager.SignOutAsync();

                    string title = "Actualizare reușită.";
                    string text = "Pentru a te putea autentifica cu noul email te rugăm să confirmi adresa de email folosind intstrucțiunile din mesaj.";
                    return RedirectToAction("Message", "Home", new { title = title, text = text});
                }
                else
                {//show error from the model
                    foreach(var error in resultUpdate.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                    return View(viewModel);
                }

            }
            return View(viewModel);
        }




        //confirm email when the logged user asked for a new email address
        [HttpGet]
        public async Task<IActionResult> ConfirmChangeEmail( string userId, string token)
        {
     
            if(userId == null || token == null)
            {
                ViewBag.MessageTitle = "A apărut o eroare și confirmarea nu a fost realizată.";
                ViewBag.MessageText = "Dacă eroarea persistă contactează-ne prin opțiunile de mai jos.";
                return View("~/Views/Home/Message.cshtml");
            }

            
            var user = await _userManager.FindByIdAsync(userId);
                        
            if (user == null)
            {
                ViewBag.MessageTitle = "Utilizatorul nu a putut fi găsit.";
                return View("~/Views/Home/Message.cshtml");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.MessageTitle = "Adresa de email a fost confirmată cu succes.";
                return View("~/Views/Home/Message.cshtml");
            }

            ViewBag.MessageTitle = "A apărut o eroare și confirmarea nu a fost realizată.";
            ViewBag.MessageText = "Mai încearcă o dată. Dacă eroarea persistă contactează-ne prin opțiunile de mai jos.";
            return View("~/Views/Home/Message.cshtml");
        }




        //returns a model with the categories for which the user has tested 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> TestsResults()
        {
            var user = await _userManager.GetUserAsync(User);
            var userTests = _context.Tests.Where(c => c.UserId == user.Id).Select(c => new { c.CategoryId, c.Category.Name, c.Date.Date }).ToList();
            var usercategories = userTests.Select(t => new { t.CategoryId, t.Name }).Distinct().ToList();
            var viewModel = new List<ProfileCategoriesViewModel>();
            foreach (var category in usercategories)
            {
                var count = userTests.Where(t => t.CategoryId == category.CategoryId).Select(t => t.Date.Date).Distinct().Count();
                //check if the user has given tests in several days to display a line chart for days
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
        




        //returns data for pie chart for all the tests
        [HttpGet]
        [Authorize]

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



        //return data for line chart in profile (results for each category and the dates on which the tests were given )
        [HttpGet]
        [Authorize]
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
            List<int> failedExamensList = new List<int>();

            LineChartDataSet lineChartDataSet = new LineChartDataSet();

            foreach(var date in dates)
            {
                passedExamensList.Add(tests.Where(c => c.Date.Date <= date).Where(c => c.Passed == true).Count());
                examensList.Add(tests.Where(c => c.Date.Date <= date).Count());
                failedExamensList.Add(tests.Where(c => c.Date.Date <= date).Where(c => c.Passed == false).Count());

            }
            int[] passedExamens = passedExamensList.ConvertAll<int>(item => (int)item).ToArray();
            int[] examens = examensList.ConvertAll<int>(item => (int)item).ToArray();
            int[] failedExamens = failedExamensList.ConvertAll<int>(item => (int)item).ToArray();
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
            LineChartDataSet failedExamendData = new LineChartDataSet()
            {
                data = failedExamens,
                label = "Numarul de examene picate",
                borderColor = "#BF381A",
                backgroundColor = "#BF381A",
                fill = false

            };
            /* = passedExamensList.ConvertAll<int>(item => (int)item).ToArray();*/
            LineChartData dataset = new LineChartData()
            {
                labels = labels,
                datasets = new LineChartDataSet[] { examensData , passedExamensData, failedExamendData }

            };
            return Json(dataset);
        }


        //return data for bar chart in profile (results for each category)
        [HttpGet]
        [Authorize]
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




        //the list of users with whom the current user had conversations 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Conversations()
        {
            var user = await _userManager.GetUserAsync(User);
            ICollection<UserConversationsViewModel> conversationsViewModels = new List<UserConversationsViewModel>();
           
            if (user != null)
            {
                ViewBag.CurrentUser = user.UserName;
                //take from db conversations of form {receiver||sender}
                var messages = await _context.Messages.Where(m => m.ReceiverUsername == user.Email || m.SenderUsername == user.Email)
                    .Select(m => new { m.ReceiverUsername, m.SenderUsername })
                    .Distinct()
                    .ToListAsync();

                //create a list of friends the user has communicated with 
                List<string> friends = new List<string>();


                foreach(var item in messages)
                {
                    if (item.ReceiverUsername != user.Email)
                    {
                        if (!friends.Contains(item.ReceiverUsername))
                            friends.Add(item.ReceiverUsername);
                    }
                    else if(item.SenderUsername != user.Email)
                    {
                        if (!friends.Contains(item.SenderUsername))
                            friends.Add(item.SenderUsername);
                    } else if(item.SenderUsername == user.Email && item.ReceiverUsername ==  user.Email)
                        {
                            if (!friends.Contains(item.SenderUsername))
                                friends.Add(item.SenderUsername);
                        }                   
                }
                //for each friend take from db the last message
                foreach(var friend in friends)
                {
                    var convo = await _context.Messages.Where(m => (m.ReceiverUsername == user.Email && m.SenderUsername == friend) || (m.ReceiverUsername == friend && m.SenderUsername == user.Email))
                        .OrderByDescending(m => m.Date)
                        .FirstOrDefaultAsync();

                    conversationsViewModels.Add(new UserConversationsViewModel
                    {
                        Name = friend,
                        Sender = convo.SenderUsername,
                        Text = convo.Text,
                        Time = convo.Date,
                        IsSeen = convo.IsSeen
                    });
                }
                //order the conersations by date
                var model = conversationsViewModels.OrderByDescending(c => c.Time).ToList();

                return View("Chat", model);
            }
            return StatusCode(404);

        }





        //returns messages with the friend and info about friend
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Chat(string friend)
        {
            if(friend != null)
            {
                //find friend in db
                var infoFriend = await _userManager.FindByNameAsync(friend);
                if (infoFriend == null)
                    return StatusCode(400);

                var user = await _userManager.GetUserAsync(User);
                ViewBag.CurrentUser = user.UserName;
                ViewBag.Friend = friend;
                //retrieve from db messages with friend
                var messages = await _context.Messages
                    .Where(m => (m.ReceiverUsername == user.UserName && m.SenderUsername == friend) || (m.ReceiverUsername == friend && m.SenderUsername == user.UserName))
                    .OrderByDescending(m => m.Date)
                    .ToListAsync();
                //mark each message as seen
                foreach (var message in messages)
                    message.IsSeen = true;
                _context.SaveChangesAsync();

                var model = messages.Select(m => new { m.SenderUsername, m.Text, m.Date}).ToList();
                return Json(model);
            }
            return StatusCode(400);
        }




        //save messages to database
        [HttpPost]
        [Authorize]        
        public async Task<IActionResult> SaveMessage(string senderUsername, string receiverUsername, string text)
        {

            if (senderUsername == null || receiverUsername == null || text == null)
            {
                return Json(new { message = "empty fields" });
            }
            Message message = new Message
            {
                SenderUsername = senderUsername,
                ReceiverUsername = receiverUsername,
                Text = text,
                IsSeen = false,
                Date = DateTime.Now
            };
            
            await _context.AddAsync(message);             
            await _context.SaveChangesAsync();            
            return Json(new { message ="ok"});
        }
    }
}
