using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IReview
	{
        public void Add(string id, int PlaceId, string msg, string name, int rating);
        public PlaceViewModel Getinfo(int id);
        public void Save();
        public void Delete(int id);
    }
}
