using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using licenta.Models;
using licenta.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = viewModel.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users =  _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> ListUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            ViewBag.RoleName = role.Name;
            var users = new List<ApplicationUser>();

            foreach (var user in _userManager.Users)
            {
                /*select users with specific roles*/
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    users.Add(user);
                }
            }
            return View("ListUsers", users);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Rolul cu acest id nu poate fi gasit";
                return View("Not found");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            
            foreach(var user in _userManager.Users)
            {
                /*select users with specific roles*/
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

/*edit role*/
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel viewModel)
        {
            var role = await _roleManager.FindByIdAsync(viewModel.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rolul cu acest id nu poate fi gasit";
                return Content("Not found");
            }
            else
            {
                role.Name = viewModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(string roleId)
        {
            ViewBag.RoleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);
            ViewBag.RoleName = role.Name;
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with this id could not be found";
                return Content("Not found");
            }

            var viewModel = new List<UserRoleViewModel>();

            foreach(var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                /*if user is selected in the given role*/
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                viewModel.Add(userRoleViewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(string roleId,List<UserRoleViewModel> viewModels)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                ViewBag.ErrorMessage = "Nu a fost gasit niciun rol cu acest Id";
                return Content("Not found");
            }

            for(int i = 0; i < viewModels.Count; i++)
            {
               var user = await _userManager.FindByIdAsync(viewModels[i].UserId);

                IdentityResult result = null;
                /*user selected but not in role, add role to user*/
                if (viewModels[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                /*if user not selected but in role, remove user*/
                }else if (!viewModels[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                /*user selected and in role, user not selected not added to role*/
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (viewModels.Count))
                        continue;
                    else
                        return RedirectToAction("ListUsersInRole", new { roleId = roleId });
                }
            }

            return RedirectToAction("ListUsersInRole", new { roleId = roleId });
        }
        public async Task<IActionResult> DeleteUser(string userId, string? roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrorMessage = "Utilizatorul cu acest id nu poate fi gasit";
                return Content("Not found");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    if(roleId == null)
                        return RedirectToAction("ListUsers");
                    
                    return RedirectToAction("ListUsersInRole", new { roleId = roleId });

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                if (roleId == null)
                    return View("ListUsers");

                return View("ListUsersInRole", new { roleId = roleId });
            }
        }
    }
}