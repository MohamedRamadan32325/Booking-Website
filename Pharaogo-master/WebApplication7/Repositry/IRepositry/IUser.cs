using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
	public interface IUser
	{
		public RegisterViewModel Update(string id);
		public bool DeleteUser(string id);
		public void UpdateUser(RegisterViewModel s);
		public string GetUser(string id);

	}
		
}
