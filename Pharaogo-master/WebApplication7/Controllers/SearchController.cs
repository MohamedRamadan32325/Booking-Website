using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication7.Data;
using WebApplication7.Repositry.IRepositry;

namespace WebApplication7.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearch _search;
        
        public SearchController(ISearch search)
        {
            _search = search;
        }

        [HttpGet]
        public IActionResult Index(string searchQuery, double MaxPrice)
        {
            var results = _search.SearchPlaces(searchQuery, MaxPrice);

            if (results == null || !results.Any())
            {
                return View("NotFound");
            }

            return View(results);
        }
    }
}
