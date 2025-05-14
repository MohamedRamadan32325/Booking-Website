using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using WebApplication7.Repositry.IRepositry;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Identity;
using WebApplication7.Repositry;

public class BookingController : Controller
{
    private readonly IPlace _placeRepository;
    private readonly IBooking _bookingRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;



    public BookingController(IPlace placeRepository,IBooking bookingRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> _signInManager, IWebHostEnvironment webHostEnvironment)



    {
        _userManager = userManager;
        signInManager = _signInManager;
        _webHostEnvironment = webHostEnvironment;
        _placeRepository = placeRepository;
        _bookingRepository = bookingRepository;

    }

    [HttpGet]
    public IActionResult Create(int id)
    {
        var bookingViewModel = _bookingRepository.Create(id);

        if (bookingViewModel == null)
        {
            return NotFound("Place not found");
        }

        return View(bookingViewModel);
    }

    [HttpPost]
    public IActionResult Create(BookingViewModel bookingViewModel,string promotioncode)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Payment", new { id = bookingViewModel.PlaceID, totalAmount = bookingViewModel.TotalAmount });
        }

        return View(bookingViewModel);
    }

   
[HttpGet]
    public IActionResult Payment( int numberofguests, BookingViewModel? model,int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "Please Login First";
            return RedirectToAction("Login", "Account");
        }

        var paymentViewModel = _bookingRepository.Payment(id,numberofguests,model.dayes, model.PromotionCode);
        var placeviewmodel = _placeRepository.Get(id);

        if (placeviewmodel == null)
        {
            return NotFound("Place not found");
        }
        if (paymentViewModel == null)
        {
            ModelState.AddModelError("PromotionCode", "Promotion Code is not valid ");
        }

        if (ModelState.IsValid)
        {

            if (placeviewmodel.SpecificPlace.Place_Type != "Hotel")
            {


                return View(paymentViewModel);
            }


            // Ensure CheckOutDate is after CheckInDate
            if (model.CheckOutDate >= model.CheckInDate)
            {
                // Calculate the difference in days
                TimeSpan difference = model.CheckOutDate - model.CheckInDate;
                model.numberofdayes = difference.Days;

                // You can now use model.NumberOfDays or pass it to a view
                if (model.numberofdayes != model.dayes)
                {
                    ModelState.AddModelError("dayes", "Number of dayes convenient ");

                }
            }
            else
            {
                ModelState.AddModelError("CheckOutDate", "CheckOutDate must be after CheckInDate.");
                ModelState.AddModelError("CheckInOutDate", "CheckOutDate must be after CheckInDate.");
            }

        }
        if (!ModelState.IsValid)
        {
            BookingViewModel bookingViewModel = new BookingViewModel
            {
                PlaceID = placeviewmodel.SpecificPlace.Place_Id,
                PlaceName = placeviewmodel.SpecificPlace.Place_Name,
                dbimage = placeviewmodel.SpecificPlace.dbimage,
                Description = placeviewmodel.SpecificPlace.Description,
                TotalAmount = placeviewmodel.SpecificPlace.Place_Price,
                PlaceType = placeviewmodel.SpecificPlace.Place_Type,
            };
            
            return View("Create", bookingViewModel);
        }

        return View(paymentViewModel);
    }


    [HttpPost]
    public IActionResult PaymentConfirmed(int TotalAmountAfterDiss)
    {
        return View("Success", TotalAmountAfterDiss);
    }
    [HttpPost]
    public IActionResult CalculateDays(BookingViewModel model)
    {
       
        return View(model);
    }

}
