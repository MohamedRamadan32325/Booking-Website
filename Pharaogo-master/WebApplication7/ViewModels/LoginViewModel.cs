using System.ComponentModel.DataAnnotations;

namespace WebApplication7.ViewModels
{
	public class LoginViewModel
	{

		public string? UserName { get; set; }
		[Required(ErrorMessage = "Email is required.")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; } // Add this property



    }
}
