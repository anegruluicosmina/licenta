using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    Birthday = registerViewModel.Birthday,

                };
               var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
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
        public IActionResult Profile()
        {
            return View();
        }
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
                user.Email = viewModel.Email;
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
    }
}
