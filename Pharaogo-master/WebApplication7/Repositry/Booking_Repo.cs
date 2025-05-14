using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Data;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;
using AutoMapper;

namespace WebApplication7.Repositry
{
    public class Booking_Repo : IBooking
    {
        private readonly DepiContext _dbContext;
        private readonly IMapper _mapper;

        public Booking_Repo(DepiContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BookingViewModel Create(int id)
        {
            Place place = _dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

            if (place == null)
            {
                return null;
            }

            // Create a booking with mapped place details
            var booking = new Booking
            {
                Place_ID = place.Place_Id,
                place = place
            };

            // Use AutoMapper to map the booking to BookingViewModel
            var bookingViewModel = _mapper.Map<BookingViewModel>(booking);
            bookingViewModel.TotalAmount = place.Place_Price;
            
            return bookingViewModel;
        }

        public PaymentViewModel Payment(int id, int numberofguests, int dayes, string PromotionCode)
        {
            Place place = _dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

            if (place == null)
            {
                return null;
            }

            // Create the payment model with basic data
            var paymentViewModel = new PaymentViewModel
            {
                TotalAmountAfterDiss = place.Place_Price,
                TotalAmount = place.Place_Price,
                PaymentCode = GeneratePaymentCode(),
                NumberOfGuests = numberofguests
            };

            // Apply promotion if provided
            if (!string.IsNullOrEmpty(PromotionCode))
            {
                var promo = _dbContext.Promotions.FirstOrDefault(x => x.promotion_Code == PromotionCode);

                if (promo != null)
                {
                    int discount = (promo.Discount_Amount * place.Place_Price) / 100;
                    paymentViewModel.TotalAmountAfterDiss = place.Place_Price - discount;
                }
                else
                {
                    return null;
                }
            }

            // Multiply by number of guests
            paymentViewModel.TotalAmountAfterDiss *= numberofguests;
            paymentViewModel.TotalAmount *= numberofguests;

            // Handle hotel bookings with days
            if (place.Place_Type == "Hotel")
            {
                paymentViewModel.TotalDayes = dayes;
                paymentViewModel.TotalAmountAfterDiss *= dayes;
                paymentViewModel.TotalAmount *= dayes;
            }

            return paymentViewModel;
        }

        private string GeneratePaymentCode()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
