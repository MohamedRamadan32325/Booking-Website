using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    /// <summary>
    /// Interface for place operations
    /// </summary>
    public interface IPlace
    {
        PlaceViewModel GetAll();
        List<Place> GetPlaces();
        PlaceViewModel GetAllMuseum();
        PlaceViewModel GetAllHotels();
        PlaceViewModel Get(int id); 
        Place GetById(int id);
        Place GetByName(string name);
        void updaterate(Place place, int rating);
        void Add(Place place);
        void Edit(PlaceViewModel place);
        void Delete(int id);
        void Save();
    }
}
