using System.ComponentModel.DataAnnotations;

namespace WebApplication7.ViewModels
{
    public class UserContactViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Message is required.")]

        public string Message { get; set; }
       
    }
}
