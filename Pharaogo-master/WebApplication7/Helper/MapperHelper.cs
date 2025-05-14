using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Helper
{
    public class MapperHelper
    {
        private readonly IMapper _mapper;

        public MapperHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Place Mappings
        
        public PlaceViewModel MapToPlaceViewModel(Place place)
        {
            return _mapper.Map<PlaceViewModel>(place);
        }

        public Place MapToPlace(PlaceViewModel viewModel)
        {
            return _mapper.Map<Place>(viewModel);
        }

        public List<PlaceViewModel> MapToPlaceViewModels(List<Place> places)
        {
            return _mapper.Map<List<PlaceViewModel>>(places);
        }
        
        #endregion

        #region User Mappings
        
        public RegisterViewModel MapToRegisterViewModel(User user)
        {
            return _mapper.Map<RegisterViewModel>(user);
        }

        public User MapToUser(RegisterViewModel viewModel)
        {
            return _mapper.Map<User>(viewModel);
        }

        public void UpdateUserFromViewModel(RegisterViewModel viewModel, User user)
        {
            _mapper.Map(viewModel, user);
        }
        
        #endregion

        #region Booking Mappings
        
        public BookingViewModel MapToBookingViewModel(Booking booking)
        {
            return _mapper.Map<BookingViewModel>(booking);
        }

        public Booking MapToBooking(BookingViewModel viewModel)
        {
            return _mapper.Map<Booking>(viewModel);
        }

        public List<BookingViewModel> MapToBookingViewModels(List<Booking> bookings)
        {
            return _mapper.Map<List<BookingViewModel>>(bookings);
        }
        
        #endregion

        #region WishList Mappings
        
        public WishLlistViewModel MapToWishListViewModel(WishList wishList)
        {
            return _mapper.Map<WishLlistViewModel>(wishList);
        }

        public WishList MapToWishList(WishLlistViewModel viewModel)
        {
            return _mapper.Map<WishList>(viewModel);
        }
        
        #endregion

        #region Payment Mappings
        
        public PaymentViewModel MapToPaymentViewModel(Payment payment)
        {
            return _mapper.Map<PaymentViewModel>(payment);
        }

        public Payment MapToPayment(PaymentViewModel viewModel)
        {
            return _mapper.Map<Payment>(viewModel);
        }
        
        #endregion

        #region Generic Mapping Methods
        
        // Generic mapping from entity to view model
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        // Generic mapping from entity to existing view model
        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            _mapper.Map(source, destination);
        }

        // Generic list mapping
        public List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
        {
            return _mapper.Map<List<TDestination>>(source);
        }
        
        #endregion
    }
} 