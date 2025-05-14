using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApplication7.Models
{
    public class Place
    {
        public Place()
        {
            dbimage = new List<byte[]>();
        }
        
        public int Place_Id { get; set; }
        public int cnt { get; set; } = 0;
        public int SumOfRates { get; set; } = 0;
        public string Place_Name { get; set; }
        public string Place_Type { get; set; }
        public string Place_City { get; set; }
        public int Place_Price { get; set; }
        public string Place_Rating { get; set; } = "unrated";
        public string? Description { get; set; }
        
        // Not mapped to database
        public List<IFormFile>? clientFile { get; set; }
        public List<byte[]>? dbimage { get; set; }
        
        // Not mapped to database
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
