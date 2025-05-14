using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication7.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication7.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPlace _placeRepository;
        private readonly IUser _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public AdminController(
            IPlace placeRepository,
            IUser userRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _placeRepository = placeRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var placesList = _placeRepository.GetPlaces();
            return View(placesList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var place = _placeRepository.Get(id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var place = _placeRepository.Get(id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(PlaceViewModel updatedPlace)
        {
            if (ModelState.IsValid)
            {
                var existingPlace = _placeRepository.Get(updatedPlace.SpecificPlace.Place_Id);
                if (existingPlace == null)
                {
                    return NotFound();
                }
                _placeRepository.Edit(updatedPlace);
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", updatedPlace);
        }

        public IActionResult Delete(int id)
        {
            _placeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Check if user exists
                var existingUser = await _userManager.FindByEmailAsync(vm.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(vm);
                }

                // Create user
                var newAdmin = new ApplicationUser
                {
                    UserName = vm.UserName,
                    Email = vm.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newAdmin, vm.Password);
                
                if (result.Succeeded)
                {
                    // Make sure role exists
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    
                    // Add to admin role
                    await _userManager.AddToRoleAsync(newAdmin, "Admin");
                    
                    TempData["SuccessMessage"] = "Admin user created successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new List<RoleViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var roleModel = new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleModel.IsSelected = true;
                }
                
                model.Add(roleModel);
            }
            
            ViewBag.UserName = user.UserName;
            ViewBag.UserId = userId;
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(List<RoleViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            
            result = await _userManager.AddToRolesAsync(user, 
                model.Where(x => x.IsSelected).Select(y => y.RoleName));
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            
            TempData["SuccessMessage"] = "Roles updated successfully";
            return RedirectToAction("Users");
        }

        // Add this method for profile viewing
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new RegisterViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View("~/Views/Account/Profile.cshtml", viewModel);
        }
    }
}