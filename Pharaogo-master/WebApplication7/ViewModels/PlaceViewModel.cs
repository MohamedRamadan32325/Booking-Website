using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication7.Models;

namespace WebApplication7.ViewModels
{
    public class PlaceViewModel
    {   public string? id {  get; set; }
        public Place? SpecificPlace { get; set; }
        
            public List<Place>? RelatedPlaces { get; set; }
        public string? Review { get; set; }
        public List<Review>? review { get; set; }
       public List<User>? user { get; set; }
        public string? Message { get; set; }


    }
}
