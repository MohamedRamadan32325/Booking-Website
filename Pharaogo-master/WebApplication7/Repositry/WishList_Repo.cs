using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Data;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
using AutoMapper;

namespace WebApplication7.Repositry
{
    public class WishList_Repo : IWishList
    {
        private readonly DepiContext _context;
        private readonly IMapper _mapper;

        public WishList_Repo(DepiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string Add(int id, string userId)
        {
            bool exists = _context.WishLists.Any(w => w.PlaceId == id && w.UserID == userId);

            if (!exists)
            {
                WishList wishList = new WishList
                {
                    PlaceId = id,
                    UserID = userId
                };

                _context.WishLists.Add(wishList);
                Save();

                return "Item added to wishlist successfully.";
            }
            else
            {
                return "This item is already in the wishlist.";
            }
        }

        public WishLlistViewModel Getwish(string userId)
        {
            // Get wishlist items with their related places
            var wishlistItems = _context.WishLists
                .Include(w => w.place)
                .Where(w => w.UserID == userId)
                .ToList();
            
            // Extract places from the wishlist items
            var places = wishlistItems.Select(w => w.place).ToList();
                
            // Create the view model
            var viewModel = new WishLlistViewModel
            {
                places = places,
                Message = places.Any() ? null : "Your wishlist is currently empty."
            };
            
            return viewModel;
        }

        public void Delete(int id, string userId)
        {
            var place = _context.WishLists
                .FirstOrDefault(w => w.PlaceId == id && w.UserID == userId);
                
            if (place != null)
            {
                _context.WishLists.Remove(place);
                Save();
            }
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
