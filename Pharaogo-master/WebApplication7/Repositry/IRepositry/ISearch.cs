using WebApplication7.Models;

namespace WebApplication7.Repositry.IRepositry
{
    public interface ISearch
    {
        public List<Place> SearchPlaces(string searchQuery,double MaxPrice);
    }
}
