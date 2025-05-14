using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IWishList
    {
        public string Add(int id, string userId);
        public WishLlistViewModel Getwish(string userid);
        public void Delete(int id, string userid);
    }
}
