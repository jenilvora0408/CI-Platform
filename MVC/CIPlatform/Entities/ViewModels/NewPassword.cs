using Entities.Models;
using System.ComponentModel.DataAnnotations;
namespace Entities.ViewModels
{
    public class NewPassword
    {
        public string email { get; set; }
        [Required]
        //[RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage = "Password is weak")]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Password does not match")]
        public string confirm_password { get; set; }
        public string token { get; set; }

        public List<Banner>? banners { get; set; }
    }
}
