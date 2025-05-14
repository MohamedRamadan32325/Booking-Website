using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApplication7.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace DEPI.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly IPlace _placeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public AdminController(IPlace placeRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> _signInManager, IWebHostEnvironment webHostEnvironment)
    
            
            
        {
                _userManager = userManager;
                signInManager = _signInManager;
                _webHostEnvironment = webHostEnvironment;
                _placeRepository = placeRepository;
        }

        public IActionResult Index()
        {
            var placesList = _placeRepository.GetPlaces();
            return View(placesList);
        }

        //[HttpGet]

        //[HttpGet]
        //public IActionResult Details(int id)
        //{
        //    var place = _placeRepository.Get(id);
        //    if (place == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(place);
        //}
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var place = _placeRepository.Get(id);
        //    if (place == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(place);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SaveEdit(PlaceViewModel updatedPlace)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingPlace = _placeRepository.Get(updatedPlace.SpecificPlace.Place_Id);
        //        if (existingPlace == null)
        //        {
        //            return NotFound();
        //        }
        //        _placeRepository.Edit(updatedPlace);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View("Edit", updatedPlace);
        //}
        //public IActionResult Delete(int id)
        //{
        //    _placeRepository.Delete(id);
        //    return RedirectToAction("Index");
        //}


        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterViewModel vm)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = vm.UserName;
                userModel.Email = vm.Email;

                //userModel.Address = vm.Addresss;
                userModel.PasswordHash = vm.Password;
                IdentityResult result = await _userManager.CreateAsync(userModel, vm.Password);
                if (result.Succeeded == true)
                {
                    //Assign Role
                    await _userManager.AddToRoleAsync(userModel, "Admin");
                    //Create Cookie

                    await signInManager.SignInAsync(userModel, isPersistent: false);


                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);
        }
    }
}