using Entities.Models;
using System.ComponentModel.DataAnnotations;
namespace Entities.ViewModels
{
    public class ForgotPassword
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid e-mail address")]
        public string email { get; set; }

        public List<Banner>? banners { get; set; } = new List<Banner>();
    }
}
