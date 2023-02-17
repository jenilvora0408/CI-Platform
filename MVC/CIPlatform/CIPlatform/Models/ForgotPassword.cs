using System.ComponentModel.DataAnnotations;
namespace CIPlatform.Models
{
    public class ForgotPassword
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid e-mail address")]
        public string email { get; set; }
    }
}
