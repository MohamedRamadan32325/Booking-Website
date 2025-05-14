using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IBooking
	{
        public BookingViewModel Create(int id);
        public PaymentViewModel Payment(int id, int numberofguests, int dayes, string PromotionCode);

    }
}
