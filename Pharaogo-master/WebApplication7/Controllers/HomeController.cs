using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlace _home;
        private readonly IReview _review;
        private readonly IUser _user;
        private readonly IWishList _wishListRepository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IPlace home, IReview review, ILogger<HomeController> logger, IUser user, IWishList wishListRepository)
        {
            _home = home;
            _review = review;
            _logger = logger;
            _user = user;
            _wishListRepository = wishListRepository;
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
        }public IActionResult Header_Hotels()
        {

            return PartialView("Places", _home.GetAllHotels());
        }

        public IActionResult GetPlace(int id)
        {
            PlaceViewModel p = new PlaceViewModel();
            p = _review.Getinfo(id);
            return View(p);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddReview(int PlaceId, string Review)
        {
            if (string.IsNullOrEmpty(Review))
            {
                ViewBag.ErrorMessage = "Review cannot be empty.";
                return RedirectToAction("GetReviews", new { PlaceId = PlaceId });
            }

            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
            {
                // Handle case where user is not authenticated
                ViewBag.ErrorMessage = "User is not authenticated.";
                return RedirectToAction("Login");
            }

            var userName = _user.GetUser(id) ?? "Unknown"; 
            _review.Add(id, PlaceId, Review, userName);

            return RedirectToAction("GetReviews", new { PlaceId = PlaceId });
        }

        public IActionResult GetReviews(string username ,string id, int PlaceId)
        {
            var p = _review.Getinfo(PlaceId);
            var temp = p.review.FirstOrDefault(x => x.UserName == username);
            ViewBag.UserNamee = username;
            return View("GetPlace", p);
        }
        public IActionResult DeleteReview(int id,int placeid)
        {
            _review.Delete(id);
            return RedirectToAction("GetPlace", new {id=placeid });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UpdateRate(int id, int rating)

        {
            Place place = _home.GetById(id);
            decimal temp;
            if (place == null)
            {
                
                return NotFound();
            }
            _home.updaterate(place, rating);
            PlaceViewModel pp = new PlaceViewModel();
            pp = _review.Getinfo(id);
            return View("GetPlace",pp);
        }
        public IActionResult FavouritePlace(int id)
        {
            var x = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _wishListRepository.Add(id, x);
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
            var wishlistItems = _wishListRepository.Getwish(id);
            return View("WishList", wishlistItems);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
