using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using AutoMapper;

namespace WebApplication7.Repositry
{
    public class Search_Repo : ISearch
    {
        private readonly DepiContext _context;
        private readonly IMapper _mapper;

        public Search_Repo(DepiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Place> SearchPlaces(string searchQuery, double maxPrice)
        {
            // Both parameters empty or zero - return empty list
            if (string.IsNullOrEmpty(searchQuery) && maxPrice == 0)
            {
                return new List<Place>();
            }
            
            // Only max price is provided - filter by price
            else if (string.IsNullOrEmpty(searchQuery) && maxPrice > 0)
            {
                var places = _context.Places
                    .Where(x => x.Place_Price >= 0 && x.Place_Price <= maxPrice)
                    .ToList();
                
                return places;
            }
            
            // Only search query is provided - filter by name or city
            else if (!string.IsNullOrEmpty(searchQuery) && maxPrice == 0)
            {
                var places = _context.Places
                    .Where(x => x.Place_Name.Contains(searchQuery) || x.Place_City.Contains(searchQuery))
                    .ToList();
                
                return places;
            }
            
            // Both parameters provided - filter by name/city and price
            else 
            {
                var places = _context.Places
                    .Where(x => (x.Place_Name.Contains(searchQuery) || x.Place_City.Contains(searchQuery)) 
                           && x.Place_Price >= 0 && x.Place_Price <= maxPrice)
                    .ToList();
                
                return places;
            }
        }
    }
}
