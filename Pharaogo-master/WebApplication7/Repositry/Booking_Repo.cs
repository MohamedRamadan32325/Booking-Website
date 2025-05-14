using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
namespace WebApplication7.Repositry
{
    public class Booking_Repo : IBooking
    {
        private readonly DepiContext dbContext;

        public Booking_Repo(DepiContext context)
        {
            dbContext = context;
        }

        public BookingViewModel Create(int id)
        {
            BookingViewModel bookingViewModel = new BookingViewModel();
            Place place = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

            if (place == null)
            {
                return null; // Or throw an exception if preferred.
            }

            bookingViewModel.PlaceID = place.Place_Id;
            bookingViewModel.PlaceName = place.Place_Name;
            bookingViewModel.dbimage = place.dbimage;
            bookingViewModel.Description = place.Description;
            bookingViewModel.TotalAmount = place.Place_Price;
            bookingViewModel.PlaceType = place.Place_Type;

            return bookingViewModel;
        }

        public PaymentViewModel Payment(int id, int numberofguests, int dayes, string PromotionCode)
        {
            PaymentViewModel pp = new PaymentViewModel();
            Place place = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

            if (place == null)
            {
                return null;
            }
            pp.TotalAmountAfterDiss = place.Place_Price;
            pp.TotalAmount = place.Place_Price;

            if (!string.IsNullOrEmpty(PromotionCode))
            {
                var promo = dbContext.Promotions.FirstOrDefault(x => x.promotion_Code == PromotionCode);

                if (promo != null)
                {
                    int temp = (promo.Discount_Amount * place.Place_Price) / 100;
                    pp.TotalAmountAfterDiss = place.Place_Price-temp  ;
                }
                else
                {
                    return null;
                }
            }
            pp.TotalAmountAfterDiss = pp.TotalAmountAfterDiss * numberofguests;
            pp.TotalAmount=pp.TotalAmount* numberofguests;
            pp.PaymentCode = GeneratePaymentCode(); 
            pp.NumberOfGuests = numberofguests;

           
            if (place.Place_Type == "Hotel")
            {
                pp.TotalDayes = dayes;
                pp.TotalAmountAfterDiss = pp.TotalAmountAfterDiss * dayes;
                pp.TotalAmount = pp.TotalAmount * dayes;
            }

            

            if (string.IsNullOrEmpty(PromotionCode))
                return pp;
            return pp;
        }
        private string GeneratePaymentCode()
        {
            
            return Guid.NewGuid().ToString();
        }

    }
}
