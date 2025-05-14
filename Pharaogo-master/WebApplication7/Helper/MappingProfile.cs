using AutoMapper;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Place mappings
            CreateMap<Place, PlaceViewModel>()
                .ForMember(dest => dest.SpecificPlace, opt => opt.MapFrom(src => src));
            CreateMap<PlaceViewModel, Place>()
                .ForMember(dest => dest.Place_Id, opt => opt.MapFrom(src => src.SpecificPlace.Place_Id))
                .ForMember(dest => dest.Place_Name, opt => opt.MapFrom(src => src.SpecificPlace.Place_Name))
                .ForMember(dest => dest.Place_Type, opt => opt.MapFrom(src => src.SpecificPlace.Place_Type))
                .ForMember(dest => dest.Place_City, opt => opt.MapFrom(src => src.SpecificPlace.Place_City))
                .ForMember(dest => dest.Place_Price, opt => opt.MapFrom(src => src.SpecificPlace.Place_Price))
                .ForMember(dest => dest.Place_Rating, opt => opt.MapFrom(src => src.SpecificPlace.Place_Rating))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.SpecificPlace.Description))
                .ForMember(dest => dest.dbimage, opt => opt.MapFrom(src => src.SpecificPlace.dbimage));

            // User mappings
            CreateMap<User, RegisterViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<RegisterViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.MobilePhone));

            // Booking mappings
            CreateMap<Booking, BookingViewModel>()
                .ForMember(dest => dest.PlaceID, opt => opt.MapFrom(src => src.Place_ID))
                .ForMember(dest => dest.PlaceName, opt => opt.MapFrom(src => src.place.Place_Name))
                .ForMember(dest => dest.PlaceType, opt => opt.MapFrom(src => src.place.Place_Type))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.place.Description))
                .ForMember(dest => dest.dbimage, opt => opt.MapFrom(src => src.place.dbimage))
                .ForMember(dest => dest.numberofdayes, opt => opt.MapFrom(src => src.total_Dayes))
                .ForMember(dest => dest.PromotionCode, opt => opt.MapFrom(src => src.promotion_Code));
            CreateMap<BookingViewModel, Booking>()
                .ForMember(dest => dest.Place_ID, opt => opt.MapFrom(src => src.PlaceID))
                .ForMember(dest => dest.total_Dayes, opt => opt.MapFrom(src => src.numberofdayes))
                .ForMember(dest => dest.promotion_Code, opt => opt.MapFrom(src => src.PromotionCode));

            // WishList mappings
            CreateMap<WishList, WishLlistViewModel>()
                .ForMember(dest => dest.places, opt => opt.Ignore());
            CreateMap<WishLlistViewModel, WishList>();

            // Payment mappings
            CreateMap<Payment, PaymentViewModel>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Amount));
            CreateMap<PaymentViewModel, Payment>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TotalAmount));

            // Review mappings
            CreateMap<Review, PlaceViewModel>()
                .ForMember(dest => dest.review, opt => opt.Ignore());

            // Login mapping
            CreateMap<LoginViewModel, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<ApplicationUser, LoginViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
} 