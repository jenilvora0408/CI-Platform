using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            /*City city = _ciPlatformContext.Cities.Where(x => x.Name == userEditProfile.CityName).FirstOrDefault();
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
                
            }*/
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
            user.Availability = userEditProfile.Availability;
            user.CityId = userEditProfile.CityId;
            user.UpdatedAt = DateTime.Now;
            _ciPlatformContext.Users.Update(user);
            _ciPlatformContext.SaveChanges();
            return user;
        }
        public editProfile PutUserDetails(editProfile model, string email)
        {
            User user = _ciPlatformContext.Users.Where(x => x.Email == email).FirstOrDefault();
            /*City city = _ciPlatformContext.Cities.Where(x => x.CityId == user.CityId).FirstOrDefault();*/
            Country country = _ciPlatformContext.Countries.Where(x => x.CountryId == user.CountryId).FirstOrDefault();
            /*if (city != null)
            {
                model.CityName = city.Name;
            }*/
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
            //model.CityId=long(user.CityId);
            model.EmployeeID = user.EmployeeId;
            if(user.CityId > 0)
            {
                long cityId = (long)user.CityId;
                model.CityId = cityId;
            }
            model.Department = user.Department;
            model.LinkedInURL = user.LinkedInUrl;
            model.Availability = user.Availability;
            model.skills = _ciPlatformContext.Skills.ToList();
            return model;
        }

        public bool ChangeUserProfileImage(string userImgPath, long userId)
        {
            User user = _ciPlatformContext.Users.Where(userid => userid.UserId.Equals(userId)).First();

            user.Avatar = userImgPath;
            _ciPlatformContext.Users.Update(user);
            _ciPlatformContext.SaveChanges();
            return true;
        }

        public VolunteeringTimesheet getTimesheet(long UserId)
        {
            VolunteeringTimesheet timesheet = new VolunteeringTimesheet();
            List<Timesheet> timesheets = _ciPlatformContext.Timesheets.Where(x => x.UserId == UserId && x.Mission.MissionType == "time").Include(x => x.Mission).ToList();
            timesheet.timesheets = timesheets.Select(t => new VolTimesheet
            {
                startDate=t.Mission.StartDate,
                endDate=t.Mission.EndDate,
                DateVolunteered = t.DateVolunteered,
                Time = t.Time,
                missionTitle = t.Mission.Title,
                missionId = t.MissionId,
                UserId = UserId,
                TimesheetId = t.TimesheetId,
                Notes = t.Notes
            }).ToList();

            List<Timesheet> GoalTimesheets = _ciPlatformContext.Timesheets.Where(x => x.UserId == UserId && x.Mission.MissionType == "goal").Include(x => x.Mission).ToList();
            timesheet.goalTimesheets = GoalTimesheets.Select(t => new VolTimesheet
            {
                startDate = t.Mission.StartDate,
                endDate = t.Mission.EndDate,
                DateVolunteered = t.DateVolunteered,
                missionTitle = t.Mission.Title,
                missionId = t.MissionId,
                UserId = UserId,
                TimesheetId = t.TimesheetId,
                Action = t.Action,
                Notes = t.Notes
            }).ToList();

            return timesheet;
        }

        public void addTimesheet(VolunteeringTimesheet volunteeringTimesheet, long UserId)
        {
            Timesheet timesheet = new Timesheet();
            timesheet.UserId = UserId;
            timesheet.MissionId = (long)volunteeringTimesheet.VolTimesheet.missionId;
            timesheet.DateVolunteered = (DateTime)volunteeringTimesheet.VolTimesheet.DateVolunteered;

            if (volunteeringTimesheet.VolTimesheet.Action == null)
            {
               if(volunteeringTimesheet.VolTimesheet.hours == null)
               {
                    volunteeringTimesheet.VolTimesheet.hours = 0;
               }

               if(volunteeringTimesheet.VolTimesheet.minutes == null)
               {
                    volunteeringTimesheet.VolTimesheet.minutes = 0;
               }

               timesheet.Time = new ((int)volunteeringTimesheet.VolTimesheet.hours, 
                   (int)volunteeringTimesheet.VolTimesheet.minutes, 0);
                
            }
            else
            {
                timesheet.Action = volunteeringTimesheet.VolTimesheet.Action;
            }

            if(volunteeringTimesheet.VolTimesheet.Notes != null)
            {
                timesheet.Notes = volunteeringTimesheet.VolTimesheet.Notes;
            }
            
            _ciPlatformContext.Add(timesheet);
            _ciPlatformContext.SaveChanges();
        }

        public void deleteTimesheet(long? timesheetId)
        {
            Timesheet timesheet = _ciPlatformContext.Timesheets.Where(x => x.TimesheetId == timesheetId).FirstOrDefault();
            if(timesheetId != null)
            {
                _ciPlatformContext.Remove(timesheet);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void updateTimesheet(VolunteeringTimesheet volunteeringTimesheet)
        {
            Timesheet timesheet = _ciPlatformContext.Timesheets.Where(x => x.TimesheetId == volunteeringTimesheet.VolTimesheet.TimesheetId).FirstOrDefault();
            if(timesheet != null)
            {
                timesheet.DateVolunteered = (DateTime)volunteeringTimesheet.VolTimesheet.DateVolunteered;
                timesheet.Notes = volunteeringTimesheet.VolTimesheet.Notes;
                if(volunteeringTimesheet.VolTimesheet.Action != null)
                {
                    timesheet.Action = volunteeringTimesheet.VolTimesheet.Action;
                }
                else
                {
                    timesheet.Time = new ((int)volunteeringTimesheet.VolTimesheet.hours,
                   (int)volunteeringTimesheet.VolTimesheet.minutes, 0);
                }
                timesheet.UpdatedAt = DateTime.Now;
                _ciPlatformContext.Update(timesheet);
                _ciPlatformContext.SaveChanges();
            }
        }


        public List<SelectListItem> GetCountryList()
        {
            return _ciPlatformContext.Countries.Select(x => new SelectListItem { Value = x.CountryId.ToString(), Text = x.Name }).ToList();
        }
        public List<SelectListItem> GetSelectedSkills(int userId)
        {
            return _ciPlatformContext.UserSkills.Where(us => us.UserId == userId).Join(_ciPlatformContext.Skills.Where(x => x.Status != false && x.DeletedAt != null), us => us.SkillId, s => s.SkillId, (us, s) => new SelectListItem { Value = s.SkillId.ToString(), Text = s.SkillName }).ToList();
        }

        public List<SelectListItem> GetNotSelectedSkills(int userId)
        {
            return _ciPlatformContext.Skills.Where(s => !_ciPlatformContext.UserSkills.Where(us => us.UserId == userId).Select(us => us.SkillId).Contains(s.SkillId)).Select(s => new SelectListItem { Value = s.SkillId.ToString(), Text = s.SkillName }).ToList();
        }


        public List<SelectListItem> GetMissionTitlesByUserIdAndType(long userId, string type)
        {
            return _ciPlatformContext.MissionApplications
                .Where(x => x.UserId == userId && x.ApprovalStatus == "Approved" && x.Mission.MissionType == type)
                .Select(s => new SelectListItem { Value = s.MissionId.ToString(), Text = s.Mission.Title })
                .ToList();
        }

        public List<City> GetCitiesOfCountry(long country)
        {
            List<City> city = _ciPlatformContext.Cities.Where(x => x.Country.CountryId == country).ToList();
            return city;
        }
        public Timesheet GetTimesheetById(int id) => _ciPlatformContext.Timesheets.Where(x => x.TimesheetId == id).Include(x => x.Mission).First();
    }
}
