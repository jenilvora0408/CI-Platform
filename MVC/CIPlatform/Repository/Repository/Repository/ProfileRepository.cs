using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Repository
{
    public class ProfileRepository : ProfileInterface
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly CiPlatformContext _ciPlatformContext;
        public ProfileRepository(CiPlatformContext ciPlatformContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public bool changePassword(editProfile edit, string email)
        {
            User userObj = _ciPlatformContext.Users.Where(u => u.Email.Equals(email)).First();
            if(userObj.Password == edit.oldPassword)
            {
                userObj.Password = edit.newPassword;
                _ciPlatformContext.Users.Update(userObj);
                _ciPlatformContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User SaveUserDetail(editProfile userEditProfile, string email)
        {
            User user = _ciPlatformContext.Users.Where(x => x.Email == email).FirstOrDefault();
            City city = _ciPlatformContext.Cities.Where(x => x.Name == userEditProfile.CityName).FirstOrDefault();
            if (city == null && userEditProfile.CityName != null)
            {
                City city1 = new City();
                city1.Name = userEditProfile.CityName;
                _ciPlatformContext.Cities.Add(city1);
                _ciPlatformContext.SaveChanges();
                City city2 = _ciPlatformContext.Cities.Where(x => x.Name == userEditProfile.CityName).FirstOrDefault();
                user.CityId = city2.CityId;
            }
            else
            {
                if(userEditProfile.CityName != null)
                {
                    user.CityId = city.CityId;
                }
                
            }
            if (userEditProfile.SkillIDs != null)
            {
                string[] skillIDStrings = userEditProfile.SkillIDs.Split(',');
                int[] skillIDs = Array.ConvertAll(skillIDStrings, int.Parse);
                List<UserSkill> userSkills = _ciPlatformContext.UserSkills.Where(x => x.UserId == user.UserId).ToList();
                List<UserSkill> skillsToRemove = userSkills.Where(x => !skillIDs.Contains(x.SkillId)).ToList();
                foreach (UserSkill skill in skillsToRemove)
                {
                    _ciPlatformContext.UserSkills.Remove(skill);
                    _ciPlatformContext.SaveChanges();
                }
                foreach (int skillID in skillIDs)
                {
                    UserSkill userSkill = _ciPlatformContext.UserSkills.Where(x => x.UserId == user.UserId && x.SkillId == skillID).FirstOrDefault();
                    if (userSkill == null)
                    {
                        userSkill = new UserSkill();
                        userSkill.UserId = user.UserId;
                        userSkill.SkillId = skillID;
                        _ciPlatformContext.UserSkills.Add(userSkill);
                        _ciPlatformContext.SaveChanges();
                    }
                }
            }
            user.FirstName = userEditProfile.FirstName;
            user.LastName = userEditProfile.LastName;
            user.Title = userEditProfile.Title;
            user.WhyIVolunteer = userEditProfile.WhyIVolunteer;
            user.ProfileText = userEditProfile.MyProfile;
            user.LinkedInUrl = userEditProfile.LinkedInURL;
            user.CountryId = userEditProfile.CountryID;
            user.EmployeeId = userEditProfile.EmployeeID;
            user.Department = userEditProfile.Department;
            user.UpdatedAt = DateTime.Now;
            _ciPlatformContext.Users.Update(user);
            _ciPlatformContext.SaveChanges();
            return user;
        }
        public editProfile PutUserDetails(editProfile model, string email)
        {
            User user = _ciPlatformContext.Users.Where(x => x.Email == email).FirstOrDefault();
            City city = _ciPlatformContext.Cities.Where(x => x.CityId == user.CityId).FirstOrDefault();
            Country country = _ciPlatformContext.Countries.Where(x => x.CountryId == user.CountryId).FirstOrDefault();
            if (city != null)
            {
                model.CityName = city.Name;
            }
            if (country != null)
            {
                model.CountryName = country.Name;
                model.CountryID = country.CountryId;
            }
            model.Email = email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Title = user.Title;
            model.WhyIVolunteer = user.WhyIVolunteer;
            model.MyProfile = user.ProfileText;
            model.UserName = user.FirstName + " " + user.LastName;
            model.EmployeeID = user.EmployeeId;
            model.Department = user.Department;
            model.LinkedInURL = user.LinkedInUrl;
            model.skills = _ciPlatformContext.Skills.ToList();
            return model;
        }
    }
}
