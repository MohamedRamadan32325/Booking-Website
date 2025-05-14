using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication7.Models
{
	public class Admin:ApplicationUser
	{

		public int Salary { get; set; }


		public string ImageUrl { get; set; }

	}
}
