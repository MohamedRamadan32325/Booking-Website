using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
namespace WebApplication7.Repositry
{
    public class Review_Repo : IReview
    {
        private readonly DepiContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public Review_Repo(DepiContext context) 
        {
            dbContext = context;
        }
        public void Delete(int id)
        {
            var review = dbContext.Review.Find(id);
            if (review != null)
            {
                dbContext.Review.Remove(review);
                Save();
            }
        }
        public void Add(string id ,int PlaceId, string msg,string username)
        {
            Review review = new Review();

            review.User_ID = id;
            review.Place_Id = PlaceId;
            review.Comment = msg;
            review.UserName = username;
            var msgg = dbContext.Review.Add(review);
            Save();
        }
        public PlaceViewModel Getinfo(int id) 
        {
            var relatedPlaces = dbContext.Places.Where(p => p.Place_Id != id).Take(4).ToList();
            var specificPlace = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);
            var reviews = dbContext.Review.Where(p => p.Place_Id == id).ToList();
           
            
            var viewModel = new PlaceViewModel
            {
                
                SpecificPlace = specificPlace,
                RelatedPlaces = relatedPlaces,
                review = reviews
            };
            return viewModel;
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
