using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class editProfile
    {
        public Navbar_1? Navbar_1 { get; set; }

        public List<CmsPage> Pages { get; set; }

        [Required]
        public string oldPassword { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,14}$", ErrorMessage = "Password must be between 6 and 14 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string newPassword { get; set; }

        [Required]
        [Compare("newPassword", ErrorMessage = "Password does not match")]
        public string confirmPassword { get; set; }

        public long? CountryID { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; }
        
        [Required]
        [StringLength(16, MinimumLength =0, ErrorMessage = "First Name cannot be more than 16 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Last Name cannot be more than 16 characters")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Employee ID cannot be more than 16 characters")]

        public string? EmployeeID { get; set; }

        public string Avatar { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Title cannot be more than 255 characters")]
        public string? Title { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Department Name cannot be more than 16 characters")]
        public string? Department { get; set; }

        public string? MyProfile { get; set; }

        public string? WhyIVolunteer { get; set; }

        public long CityId { get; set; }

        public string CountryName { get; set; }

        public string? Availability { get; set; }

        public string? LinkedInURL { get; set; }

        public string? SkillIDs { get; set; }

        [Required]
        [StringLength(60000, MinimumLength = 0, ErrorMessage = "Message cannot be more than 60000 characters")]
        public string? Message { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 0, ErrorMessage = "Subject cannot be more than 255 characters")]
        public string? Subject { get; set; }

        public List<Skill> skills { get; set; }
    }
}
