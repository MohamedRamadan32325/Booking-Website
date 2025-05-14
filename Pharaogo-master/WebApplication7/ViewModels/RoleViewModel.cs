using System.ComponentModel.DataAnnotations;

namespace WebApplication7.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        
        [Required(ErrorMessage = "Role name is required.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        
        public bool IsSelected { get; set; }
    }
}
