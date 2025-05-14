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
using AutoMapper;
using WebApplication7.Helper;

namespace WebApplication7.Controllers
{
    public class BookingController : Controller
    {
        private readonly IPlace _placeRepository;
        private readonly IBooking _bookingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly MapperHelper _mapperHelper;

        public BookingController(
            IPlace placeRepository,
            IBooking bookingRepository, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            MapperHelper mapperHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _placeRepository = placeRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _mapperHelper = mapperHelper;
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
        public IActionResult Create(BookingViewModel bookingViewModel, string promotionCode)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Payment", new { 
                    id = bookingViewModel.PlaceID, 
                    totalAmount = bookingViewModel.TotalAmount,
                    promotionCode = promotionCode
                });
            }

            return View(bookingViewModel);
        }

        [HttpGet]
        public IActionResult Payment(int id, decimal totalAmount, string promotionCode, int numberOfGuests = 1, int days = 1)
        {
            var paymentViewModel = _bookingRepository.Payment(id, numberOfGuests, days, promotionCode);
            
            if (paymentViewModel == null)
            {
                return NotFound("Invalid promotion code or place not found");
            }
            
            return View(paymentViewModel);
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel paymentViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirmation");
            }
            
            return View(paymentViewModel);
        }

        public IActionResult Confirmation()
        {
            return View();
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
}
