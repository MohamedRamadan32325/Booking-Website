using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.ViewModels
{
    public class BookingViewModel
    {
        public int dayes { get; set; }

        public int PlaceID { get; set; }  // Place ID (can be optional)
        public string? PlaceType { get; set; }
        public string PlaceName { get; set; } = "Default Place";  // Default place name if no place is selected
        public List<byte[]>? dbimage { get; set; }

        public List<string>? imageSrc
        {
            get
            {
                if (dbimage == null || dbimage.Count == 0)
                {
                    return null;
                }
                List<string> base64Images = new List<string>();
                foreach (var db in dbimage)
                {
                    if (db != null)
                    {
                        string base64String = Convert.ToBase64String(db, 0, db.Length);
                        base64Images.Add("data:image/jpg;base64," + base64String);
                    }
                }
                return base64Images;
            }
        }
        public string Description { get; set; } = "No specific place selected. Please select a place.";

        [Required]
        [Display(Name = "Check-in Date")]
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime AvilableDate { get; set; } = DateTime.Now;


        [Required]
        [Display(Name = "Check-out Date")]
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);
        public int numberofdayes {  get; set; }
      
        [Required]
        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; } = 1;

        [Display(Name = "Promotion Code")]
        public string? PromotionCode { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; } // Correct property name
        public int BookingId { get; set; } // This will hold the booking ID
    }
}





