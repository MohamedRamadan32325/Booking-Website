using WebApplication7.Data;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;

namespace WebApplication7.Repositry
{
    public class Promotion_Repo : IPromotion
    {
        private readonly DepiContext _context;
        
        public Promotion_Repo(DepiContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Promotion> GetAll()
        {
            return _context.Promotions.ToList();
        }

        public Promotion Get(int id)
        {
            return _context.Promotions.FirstOrDefault(p => p.promotion_Id == id);
        }

        public void Add(Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            Save();
        }
        
        public void Delete(int id)
        {
            var promotion = _context.Promotions.FirstOrDefault(p => p.promotion_Id == id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
