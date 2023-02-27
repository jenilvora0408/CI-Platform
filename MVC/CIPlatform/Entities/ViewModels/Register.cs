using System.ComponentModel.DataAnnotations;
namespace Entities.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "First Name is required")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string last_name { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "please enter valid phone number")]
        public long phone_number { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid e-mail address")]
        public string email { get; set; }

        [Required]
        //[RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage = "Password is weak")]
        public string password { get; set; }

        [Required]
        //[Compare("password", ErrorMessage = "Password does not match")]
        public string confirm_password { get; set; }
    }
}
