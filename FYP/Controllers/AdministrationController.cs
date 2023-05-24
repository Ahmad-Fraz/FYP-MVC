using DataBase;
using FYP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FYP.Controllers
{
    public class AdministrationController : Controller
    {
        int Addnotify = 1, RemNotify = 1;
        public string Add_notification { get; set; }
        public string? Remove_notification { get; set; }
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DBase dBase;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, DBase dBase)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.dBase = dBase;
        }


        public IActionResult Roles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult CreateRoles()
        {
            return PartialView("CreateRoles");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(CreateRoles createRoles)
        {

            if (ModelState.IsValid)
            {
                var UserRole = new IdentityRole();
                UserRole.Name = createRoles.RoleName;

                var result = await roleManager.CreateAsync(UserRole);
                if (result.Succeeded)
                {
                    TempData["roleAdded"] = "true";
                    return RedirectToAction("RolesList", "Administration");

                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(createRoles);
        }


        public async Task<IActionResult> EditRole(string? id, string? Add_notification, string? Remove_notification)
        {
            ViewBag.AddNotify = Add_notification;
            ViewBag.RemNotify = Remove_notification;
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ModelState.AddModelError("", $"id is not valid {id}");
            }

            var model = new EditRole()
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRole editRole)
        {

            var role = await roleManager.FindByIdAsync(editRole.Id);
            if (role == null)
            {
                ModelState.AddModelError("", $"Role with id {editRole.Id} cannot be found");
            }
            else
            {
                role.Name = editRole.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    TempData["roleUpdate"] = "true";
                    return RedirectToAction("RolesList", "Administration");

                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(editRole);
        }

        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["roledel"] = "true";
            }

            return RedirectToAction(nameof(RolesList));

        }

        [HttpGet]
        public async Task<IActionResult> AddUsers(string id)
        {
            ViewBag.roleId = id;
            var role = await roleManager.FindByIdAsync(id);
            ViewBag.roleName = role.Name;
            if (role == null)
            {
                ModelState.AddModelError("", $"id is not valid {id}");
            }
            var model = new List<AddUsersToRoles>();
            foreach (var user in userManager.Users)
            {
                var AddUsers = new AddUsersToRoles()
                {
                    id = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    AddUsers.isMember = true;
                }
                else
                {
                    AddUsers.isMember = false;
                }
                model.Add(AddUsers);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers(List<AddUsersToRoles> users, string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ModelState.AddModelError("", $"Role with id = {id} cannot be found.");
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < users.Count; i++)
                {

                    var user = await userManager.FindByIdAsync(users[i].id);
                    if (users[i].isMember && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {

                        Addnotify += Addnotify;
                        var result = await userManager.AddToRoleAsync(user, role.Name);
                        if (result.Succeeded)
                        {
                            if (Addnotify == 2)
                                Add_notification = "1add";
                            else
                                Add_notification = "2add";
                        }
                    }

                    else if (!users[i].isMember && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        var result = await userManager.RemoveFromRoleAsync(user, role.Name);
                        if (result.Succeeded)
                        {
                            RemNotify += RemNotify;
                            if (RemNotify == 2)
                                Remove_notification = "1rem";
                            else
                                Remove_notification = "2rem";
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return RedirectToAction(nameof(EditRole), new { id = id, Add_notification, Remove_notification });
            }
            return View();
        }



        public IActionResult RolesList()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
    }
}
