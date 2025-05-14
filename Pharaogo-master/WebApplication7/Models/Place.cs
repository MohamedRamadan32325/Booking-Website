using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.Arm;

namespace WebApplication7.Models
{
    public class Place
    {
        public Place()
        {
            dbimage = new List<byte[]>();
        }
        [Key]
        public int Place_Id { get; set; }
        public int cnt { get; set; } = 0;
        public int SumOfRates { get; set; } = 0;

        [Required]
        [Display(Name = "Place Name : ")]

        public string Place_Name { get; set; }
        [Required]
        [Display(Name = "Type :")]
        public string Place_Type { get; set; }
        [Display(Name = "City :")]
        public string Place_City { get; set; }
        [Required]
        [Display(Name = "Price :")]

        public int Place_Price { get; set; }
        [Display(Name = "Rating :")]

        public string Place_Rating { get; set; } = "unrated";
        public string? Description { get; set; }
        [NotMapped]
        public List<IFormFile>? clientFile { get; set; }

        public List<byte[]>? dbimage { get; set; }

        [NotMapped]
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
    }
}
