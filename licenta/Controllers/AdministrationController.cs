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
        public async Task<IActionResult> ListUsers(string search, string order, int minage, int maxage, int? page, string roleId)
        {

            if (minage != 0)
                ViewData["MinAge"] = minage;

            if (maxage != 0)
                ViewData["MaxAge"] = maxage;

            ViewData["SearchString"] = search;
            ViewData["Order"] = order;

            if (maxage <= 0)
                maxage = 150;

            //retrieve users form db
            IEnumerable<ApplicationUser> users;

            if (!String.IsNullOrWhiteSpace(roleId))
            {
                ViewBag.roleId = roleId;
                var role = await _roleManager.FindByIdAsync(roleId);
                ViewBag.RoleName = role.Name;
                IList<ApplicationUser> usersRole = new List<ApplicationUser>();

                foreach (var user in _userManager.Users)
                {
                    /*select users with specific roles*/
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        usersRole.Add(user);
                    }
                }
                users = usersRole.ToList();
            }
            else
            {
                users = _userManager.Users
                             .Where(u => u.Age >= minage && u.Age <= maxage).ToList();
            }

            //apply search restrictions 
            if (search != null)
            {
                search = search.Trim();
                users = users.Where(u => u.Email.Contains(search) || u.FirstName.Contains(search) || u.LastName.Contains(search) || String.Concat(u.LastName , " " , u.FirstName).Contains(search) || String.Concat(u.FirstName , " " , u.LastName).Contains(search));
            }
            //order the result 
            if(!String.IsNullOrWhiteSpace(order))
            {
                order = order.Trim();

                if (order.Equals("ASC"))
                {
                    users = users.OrderBy(u => u.FirstName);
                    ViewData["AscChecked"] = "checked"; //used to check radio button when the page refreshes 
                }
                else if(order.Equals("DESC"))
                {
                    users = users.OrderByDescending(u => u.FirstName);
                    ViewData["DescChecked"] = "checked";//used to check radio button when the page refreshes 
                }

            }
            //feed the model with the items for the specific index of the page
            var model = await PaginatedList<ApplicationUser>.CreateAsync(users, page ?? 1,12);

            return View(model);
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