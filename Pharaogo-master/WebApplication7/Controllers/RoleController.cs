using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
	

	public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> _rolemanager)
        {
            roleManager = _rolemanager;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel newRole)
        {
            if (ModelState.IsValid == true)
            {
                IdentityRole role = new IdentityRole() { Name = newRole.RoleName };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }
            }
            return View(newRole);
        }
    }
}
