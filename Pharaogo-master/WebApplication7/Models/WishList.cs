namespace WebApplication7.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public Place place { get; set; }
        public string UserID { get; set; }
        public User user { get; set; }
    }
}
