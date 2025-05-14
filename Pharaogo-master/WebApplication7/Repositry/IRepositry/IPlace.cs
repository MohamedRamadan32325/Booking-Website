using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IPlace
	{
        public PlaceViewModel GetAll();
        public List<Place> GetPlaces();

        public PlaceViewModel GetAllMuseum();
        public PlaceViewModel GetAllHotels();
        public PlaceViewModel Get(int id); 
        public Place GetById(int id);
        public Place GetByName(string  Name);
        public void updaterate(Place place, int rating);
        public void Add(Place place);
        public void Edit(PlaceViewModel place);
        public void Delete(int id);
        public void Save();
        
    }
}
