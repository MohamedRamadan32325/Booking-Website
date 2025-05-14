using WebApplication7.Models;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IPromotion
	{
        IEnumerable<Promotion> GetAll();
        Promotion Get(int id);
        void Add(Promotion promotion);
        void Delete(int id);
        void Save();
    }
}
