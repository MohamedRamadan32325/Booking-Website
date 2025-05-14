using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Data;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
using AutoMapper;

namespace WebApplication7.Repositry
{
    public class Place_Repo : IPlace
    {
        private readonly DepiContext _context;
        private readonly IMapper _mapper;

        public Place_Repo(DepiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PlaceViewModel GetAll()
        {
            var places = _context.Places.ToList();
            var viewModel = new PlaceViewModel
            {
                RelatedPlaces = places
            };
            return viewModel;
        }

        public List<Place> GetPlaces()
        {
            return _context.Places.ToList();
        }

        public PlaceViewModel GetAllMuseum()
        {
            var museums = _context.Places.Where(x => x.Place_Type == "Museum").ToList();
            var viewModel = new PlaceViewModel
            {
                RelatedPlaces = museums
            };
            return viewModel;
        }

        public PlaceViewModel GetAllHotels()
        {
            var hotels = _context.Places.Where(x => x.Place_Type == "Hotel").ToList();
            var viewModel = new PlaceViewModel
            {
                RelatedPlaces = hotels
            };
            return viewModel;
        }

        public Place GetById(int id)
        {
            return _context.Places.FirstOrDefault(x => x.Place_Id == id);
        }

        public Place GetByName(string Name)
        {
            return _context.Places.FirstOrDefault(x => x.Place_Name == Name);
        }

        public PlaceViewModel Get(int id)
        {
            var relatedPlaces = _context.Places.Where(p => p.Place_Id != id).Take(8).ToList();
            var specificPlace = _context.Places.FirstOrDefault(x => x.Place_Id == id);
            
            // Create view model using AutoMapper for specific place
            var viewModel = new PlaceViewModel
            {
                SpecificPlace = specificPlace,
                RelatedPlaces = relatedPlaces
            };
            
            return viewModel;
        }

        public void Add(Place place)
        {
            if (place.clientFile != null)
            {
                foreach (var file in place.clientFile)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        place.dbimage.Add(stream.ToArray());
                    }
                }
            }
            _context.Places.Add(place);
            Save();
        }

        public void updaterate(Place place, int rating)
        {
            place.cnt += 1;
            place.SumOfRates += rating;
            Save();
            
            decimal temp = place.SumOfRates / place.cnt;
            place.Place_Rating = temp.ToString();
            Save();
        }

        public void Edit(PlaceViewModel placeViewModel)
        {
            var existingPlace = _context.Places.Find(placeViewModel.SpecificPlace.Place_Id);
            if (existingPlace != null)
            {
                // Map properties using AutoMapper
                _mapper.Map(placeViewModel.SpecificPlace, existingPlace);
                
                // Handle file uploads separately as they require special processing
                if (placeViewModel.SpecificPlace.clientFile != null)
                {
                    existingPlace.dbimage = new List<byte[]>();
                    foreach (var file in placeViewModel.SpecificPlace.clientFile)
                    {
                        using (var stream = new MemoryStream())
                        {
                            file.CopyTo(stream);
                            existingPlace.dbimage.Add(stream.ToArray());
                        }
                    }
                }
                
                Save();
            }
        }

        public void Delete(int id)
        {
            var place = _context.Places.Find(id);
            if (place != null)
            {
                _context.Places.Remove(place);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
