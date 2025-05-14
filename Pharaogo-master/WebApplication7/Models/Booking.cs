namespace WebApplication7.Models
{
    public class Booking
    {
        public int booking_Id { get; set; }
        public string User_ID { get; set; }
        public User user { get; set; }
        public int Place_ID { get; set; }
        public Place place { get; set; }
        public string? promotion_Code { get; set; }
        public bool? payment_state { get; set; }
        public int total_amount { get; set; }
        public int? total_Dayes { get; set; }
    }
}
