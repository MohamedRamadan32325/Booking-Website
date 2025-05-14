using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;  // Assuming you're using Entity Framework
using System.Linq;
using WebApplication7.Repositry.IRepositry;
using Microsoft.IdentityModel.Tokens;

public class SearchController : Controller
{
    private readonly DepiContext _context; // DbContext to interact with the database
    ISearch _search;
    public SearchController(DepiContext context, ISearch tt)
    {
        _context = context;
        _search = tt;
    }

    [HttpGet]
    public IActionResult Index(string searchQuery,double MaxPrice)
    {
        var results = _search.SearchPlaces(searchQuery,MaxPrice);

        if (results == null || !results.Any())
        {
            return View("NotFound");
        }

        return View(results);
    }

}
