using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Data;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
using AutoMapper;

namespace WebApplication7.Repositry
{
    public class Review_Repo : IReview
    {
        private readonly DepiContext _dbContext;
        private readonly IMapper _mapper;

        public Review_Repo(DepiContext context, IMapper mapper) 
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            var review = _dbContext.Review.Find(id);
            if (review != null)
            {
                _dbContext.Review.Remove(review);
                Save();
            }
        }

        public void Add(string id, int PlaceId, string msg, string username, int rating)
        {
            Review review = new Review
            {
                User_ID = id,
                Place_Id = PlaceId,
                Comment = msg,
                UserName = username,
                Rating = rating,
                Date = DateTime.Now
            };
            
            _dbContext.Review.Add(review);
            Save();
        }

        public PlaceViewModel Getinfo(int id) 
        {
            // Get the specific place
            var specificPlace = _dbContext.Places.FirstOrDefault(x => x.Place_Id == id);
            
            // Get related places
            var relatedPlaces = _dbContext.Places
                .Where(p => p.Place_Id != id)
                .Take(4)
                .ToList();
            
            // Get reviews for the place
            var reviews = _dbContext.Review
                .Where(p => p.Place_Id == id)
                .ToList();
            
            // Create view model
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
            _dbContext.SaveChanges();
        }
    }
}
