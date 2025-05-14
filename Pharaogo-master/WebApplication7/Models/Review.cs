namespace WebApplication7.Models
{
    public class Review
    {
        public int Review_Id { get; set; }
        public string? Comment { get; set; }
        public string User_ID { get; set; }
        public User user { get; set; }
        public int Place_Id { get; set; }
        public Place place { get; set; }
        public int Rating { get; set; }
        public string? UserName { get; set; }
        public DateTime? Date { get; set; }
    }
}
