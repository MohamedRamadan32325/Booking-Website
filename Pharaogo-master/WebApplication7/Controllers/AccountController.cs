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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> _signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            signInManager = _signInManager;
            _webHostEnvironment = webHostEnvironment;


        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel Userrvm)
        {
            if (ModelState.IsValid)
            {
                // Check if the email already exists
                var existingUserByEmail = await _userManager.FindByEmailAsync(Userrvm.Email);
                if (existingUserByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return View(Userrvm);
                }

                // Check if the username already exists
                var existingUserByUsername = await _userManager.FindByNameAsync(Userrvm.UserName);
                if (existingUserByUsername != null)
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                    return View(Userrvm);
                }

                // Create a new user
                User userModel = new User
                {
                    UserName = Userrvm.UserName,
                    Email = Userrvm.Email
                };

                // Create the user in the system
                IdentityResult result = await _userManager.CreateAsync(userModel, Userrvm.Password);

                if (result.Succeeded)
                {
                    // Assign role based on email
                    if (Userrvm.Email == "admiin@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(userModel, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(userModel, "Visitor");
                    }

                    // Sign the user in
                    await signInManager.SignInAsync(userModel, isPersistent: false);

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
            return View(Userrvm);
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
                ApplicationUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {
                    // Check if the password is correct
                    bool found = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (found)
                    {
                        // Sign in the user using SignInManager
                        await signInManager.SignInAsync(user, loginViewModel.RememberMe);

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
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}