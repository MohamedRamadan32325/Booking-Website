using System.ComponentModel.DataAnnotations;

namespace WebApplication7.ViewModels
{
	public class RegisterViewModel
	{
		public string? Id { get; set; }
		public string? LastName { get; set; }
		public string? MobilePhone { get; set; }
		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
     ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string confirmPassword { get; set; }


	}
}
