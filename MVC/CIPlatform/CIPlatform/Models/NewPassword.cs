using System.ComponentModel.DataAnnotations;
namespace CIPlatform.Models
{
    public class NewPassword
    {
        [Required]
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage = "Password is weak")]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Password does not match")]
        public string confirm_password { get; set; }
    }
}
