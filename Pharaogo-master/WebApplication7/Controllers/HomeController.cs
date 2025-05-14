using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
using AutoMapper;
using WebApplication7.Helper;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlace _home;
        private readonly IReview _review;
        private readonly IUser _user;
        private readonly IWishList _wishListRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly MapperHelper _mapperHelper;

        public HomeController(
            IPlace home, 
            IReview review, 
            ILogger<HomeController> logger, 
            IUser user, 
            IWishList wishListRepository,
            IMapper mapper,
            MapperHelper mapperHelper)
        {
            _home = home;
            _review = review;
            _logger = logger;
            _user = user;
            _wishListRepository = wishListRepository;
            _mapper = mapper;
            _mapperHelper = mapperHelper;
        }

        public IActionResult Index()
        {
            var places = _home.GetAll();
            return View(places);
        }

        public IActionResult Museums()
        {
            return PartialView("Details", _home.GetAllMuseum());
        }

        public IActionResult Hotels()
        {
            return PartialView("Details", _home.GetAllHotels());
        }

        public IActionResult Header_Museums()
        {
            return PartialView("Places", _home.GetAllMuseum());
        }

        public IActionResult Header_Hotels()
        {
            return PartialView("Places", _home.GetAllHotels());
        }

        public IActionResult GetPlace(int id)
        {
            var placeViewModel = _review.Getinfo(id);
            return View(placeViewModel);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AddReview(string id, int PlaceId, string msg, string username, int rating)
        {
            _review.Add(id, PlaceId, msg, username, rating);
            PlaceViewModel p = new PlaceViewModel();
            p = _review.Getinfo(PlaceId);
            return View("GetPlace", p);
        }

        public IActionResult RateTour(int id, int rating)
        {
            Place place = _home.GetById(id);
            if (place == null)
            {
                return NotFound();
            }
            
            _home.updaterate(place, rating);
            
            // Use AutoMapper to get the place view model
            var placeViewModel = _review.Getinfo(id);
            return View("GetPlace", placeViewModel);
        }

        public IActionResult FavouritePlace(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _wishListRepository.Add(id, userId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWish(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _wishListRepository.Delete(id, userId);
            return RedirectToAction("WishList", new { id = userId });
        }

        public IActionResult WishList(string id)
        {
            var wishlistViewModel = _wishListRepository.Getwish(id);
            return View("WishList", wishlistViewModel);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
