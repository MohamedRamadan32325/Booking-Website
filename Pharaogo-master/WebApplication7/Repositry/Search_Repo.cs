using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;

namespace WebApplication7.Repositry
{
    public class Search_Repo : ISearch
    {
        private readonly DepiContext _context;

        public Search_Repo(DepiContext context)
        {
            _context = context;
        }

        public List<Place> SearchPlaces(string searchQuery,double MaxPrice)
        {
            if (string.IsNullOrEmpty(searchQuery) && MaxPrice == 0)
            {
                return new List<Place>();
            }
            else if (string.IsNullOrEmpty(searchQuery) && MaxPrice > 0)
            {
                return _context.Places.Where(x => x.Place_Price >= 0 && x.Place_Price <= MaxPrice).ToList();
            }
            else if (searchQuery!=null && MaxPrice == 0)
            {
                return _context.Places
              .Where(x => x.Place_Name.Contains(searchQuery) || x.Place_City.Contains(searchQuery))
              .ToList();
            }
            else 
            {
                return _context.Places
               .Where(x => x.Place_Name.Contains(searchQuery) || x.Place_City.Contains(searchQuery) && x.Place_Price >= 0 && x.Place_Price <= MaxPrice)
               .ToList();
            }

        }
    }
}
