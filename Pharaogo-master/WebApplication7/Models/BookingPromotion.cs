using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class BookingPromotion
    {
        public int Id { get; set; }
        [ForeignKey("Booking")]
        public int Booking_Id {  get; set; }
        public Booking book {  get; set; }
        [ForeignKey("Promotion")]
        public int Promotion_Id {  get; set; }
        public Promotion promotion {  get; set; }

    }
}
