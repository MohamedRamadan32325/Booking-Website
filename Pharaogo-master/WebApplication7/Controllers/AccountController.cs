using WebApplication7.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager, 
                               SignInManager<ApplicationUser> signInManager, 
                               RoleManager<IdentityRole> roleManager,
                               IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
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
                // Check if the email already exists
                var existingUserByEmail = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (existingUserByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return View(registerViewModel);
                }

                // Check if the username already exists
                var existingUserByUsername = await _userManager.FindByNameAsync(registerViewModel.UserName);
                if (existingUserByUsername != null)
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                    return View(registerViewModel);
                }

                // Create a new ApplicationUser
                var newUser = new ApplicationUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true
                };

                // Create the user in the system
                IdentityResult result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

                if (result.Succeeded)
                {
                    // Check if the admin role exists
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Check if the visitor role exists
                    if (!await _roleManager.RoleExistsAsync("Visitor"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Visitor"));
                    }

                    // Assign role based on email
                    var adminEmails = new[] { "admin@pharaogo.com", "admiin@gmail.com" };
                    if (adminEmails.Contains(registerViewModel.Email.ToLower()))
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, "Visitor");
                    }

                    // Sign the user in
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle errors in user creation
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // If validation fails, return the view with the model data
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                // Find user by email
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {
                    // Check if the password is correct
                    var result = await _signInManager.PasswordSignInAsync(
                        user, 
                        loginViewModel.Password, 
                        loginViewModel.RememberMe, 
                        lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Redirect to the home page after login
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Password is incorrect
                        ModelState.AddModelError("Password", "Your password is incorrect.");
                    }
                }
                else
                {
                    // User does not exist
                    ModelState.AddModelError("Email", "User does not exist.");
                }
            }

            // If ModelState is not valid, return the view with errors
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var viewModel = new RegisterViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(viewModel);
        }
    }
}