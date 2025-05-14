using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.ViewModels;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(RoleViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                // Check if role already exists
                var roleExists = await _roleManager.RoleExistsAsync(newRole.RoleName);
                if (roleExists)
                {
                    ModelState.AddModelError("RoleName", "Role name already exists");
                    return View(newRole);
                }

                var role = new IdentityRole { Name = newRole.RoleName };
                var result = await _roleManager.CreateAsync(role);
                
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View(newRole);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return NotFound();
                }

                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            
            // Check if users are assigned to this role
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete role as it has users assigned to it";
                return RedirectToAction(nameof(Index));
            }
            
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role deleted successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the role";
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
