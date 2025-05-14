namespace WebApplication7.Models
{
    public class Payment
    {
        public int Payment_Id { get; set; }
        public int booking_Id { get; set; }
        public Booking booking { get; set; }
        public int Amount { get; set; }
    }
}
