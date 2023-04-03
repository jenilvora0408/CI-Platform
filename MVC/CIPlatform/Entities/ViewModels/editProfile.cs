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
        [Required]
        public string oldPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        [Compare("newPassword", ErrorMessage = "Password does not match")]
        public string confirmPassword { get; set; }
        public long? CountryID { get; set; }

        public long UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? EmployeeID { get; set; }
        public string Avatar { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? MyProfile { get; set; }
        public string? WhyIVolunteer { get; set; }
        public string? CityName { get; set; }
        public string CountryName { get; set; }
        public string? Availability { get; set; }
        public string? LinkedInURL { get; set; }
        public string? SkillIDs { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public List<Skill> skills { get; set; }
    }
}
