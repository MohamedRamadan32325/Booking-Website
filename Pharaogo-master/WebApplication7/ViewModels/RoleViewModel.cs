using System.ComponentModel.DataAnnotations;

namespace WebApplication7.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get; set; }
    }
}
