using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
namespace WebApplication7.Repositry
{
    public class WishList_Repo : IWishList
    {
        DepiContext _context;
        public WishList_Repo(DepiContext _context)
        {
            this._context = _context;
        }
        public string Add(int id, string userId)
        {
            bool exists = _context.wishLists.Any(w => w.PlaceId == id && w.UserID == userId);

            if (!exists)
            {
                WishList wishList = new WishList
                {
                    PlaceId = id,
                    UserID = userId
                };

                _context.wishLists.Add(wishList);
                Save();

                return "Item added to wishlist successfully.";
            }
            else
            {
                return "This item is already in the wishlist.";
            }
        }

        public WishLlistViewModel Getwish(string userid)
        {
            var list = _context.wishLists
            .Include(w => w.place)
            .Where(w => w.UserID == userid)
            .Select(w => new Place
            {
                // Assuming PlaceViewModel has these properties, map them accordingly
                Place_Name = w.place.Place_Name,
                Place_City = w.place.Place_City,
                dbimage = w.place.dbimage,
                Place_Price = w.place.Place_Price,
                Place_Rating = w.place.Place_Rating,
                Place_Type = w.place.Place_Type,
                Place_Id = w.place.Place_Id,
                // Add more properties as needed based on PlaceViewModel structure
            })
            .ToList();
            WishLlistViewModel wl = new WishLlistViewModel
            {
                places = list,
                Message = list.Any() ? null : "Your wishlist is currently empty."
            };
            return wl;
        }
        public void Delete(int id, string userid)
        {
            var place = _context.wishLists
                .FirstOrDefault(w => w.PlaceId == id && w.UserID == userid);
            if (place != null)
            {
                _context.wishLists.Remove(place);
                Save();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
