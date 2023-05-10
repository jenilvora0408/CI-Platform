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
    public class AdminRepository : AdminInterface
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly CiPlatformContext _ciPlatformContext;
        public AdminRepository(CiPlatformContext ciPlatformContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public CMS GetCmsPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 2;
            CMS cms = new CMS();
            bool status = true;
            var cmsPages = new List<CmsPage>();
            cmsPages = _ciPlatformContext.CmsPages.Where(x => x.Status == status && x.DeletedAt == null).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                cmsPages = cmsPages.Where(x => x.Title.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)cmsPages.Count / pageSize);
            List<CmsPage> pagedUsers = cmsPages
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            cms.cmsPage = pagedUsers;
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;
            return cms;
        }

        public CMS GetBannerPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 2;
            CMS cms = new CMS();
            var bannerPages = new List<Banner>();
            bannerPages = _ciPlatformContext.Banners.Where(x => x.DeletedAt == null).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                bannerPages = bannerPages.Where(x => x.Text.ToLower().Contains(Search.ToLower()) || x.SortOrder.ToString() == Search ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)bannerPages.Count / pageSize);
            List<Banner> pagedUsers = bannerPages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            cms.Banner = pagedUsers;
            cms.PageCount = totalCount;
            cms.PageSize =pageSize;
            cms.CurrentPage = pageNumber;

            return cms;
        }

        public CMS GetUserPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 9;
            CMS cms = new CMS();
            var user = new List<User>();
            user = _ciPlatformContext.Users.Where(x => x.DeletedAt == null).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                user = user.Where(x => x.FirstName.ToLower().Contains(Search.ToLower()) ||
                x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)user.Count / pageSize);
            List<User> pagedUsers = user
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            cms.user = pagedUsers;
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;
            return cms;
        }

        public CMS GetMissionPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 5;
            CMS cms = new CMS();
            var mission = _ciPlatformContext.Missions.Where(x => x.DeletedAt == null).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                mission = mission.Where(x => x.Title.ToLower().Contains(Search.ToLower()) ||
                x.MissionType.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)mission.Count / pageSize);
            List<Mission> pagedUsers = mission
                .Skip((pageNumber - 1) *pageSize)
                .Take(pageSize)
                .ToList();
            cms.missions = pagedUsers.Select(x => new Mission()
            {
                Title = x.Title,
                MissionId = x.MissionId,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                MissionType = x.MissionType,
            }).ToList();
            cms.missions = pagedUsers;
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;
            return cms;
        }

        public CMS GetStoryPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 3;
            CMS cms = new CMS();
            var story = new List<Story>();
            story = _ciPlatformContext.Stories.Where(x => x.Status == "Pending" && x.DeletedAt == null).Include(x => x.User).Include(x => x.Mission).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                story = story.Where(x => x.Title.ToLower().Contains(Search.ToLower())).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)story.Count / pageSize);
            List<Story> pagedUsers = story
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            cms.stories = pagedUsers.Select(x => new Story()
            {
                Title = x.Title,
                Mission = x.Mission,
                User = x.User,
                StoryId = x.StoryId,
            }).ToList();    
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;

            return cms;
        }

        public CMS GetSkillPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 4;
            CMS cms = new CMS();
            var skill = new List<Skill>();
            skill = _ciPlatformContext.Skills.Where(x => x.DeletedAt == null).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                skill = skill.Where(x => x.SkillName.ToLower().Contains(Search.ToLower())).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)skill.Count / pageSize);
            List<Skill> pagedUsers = skill
                .Skip((pageNumber - 1) *pageSize)
                .Take(pageSize)
                .ToList();
            cms.Skills = pagedUsers.Select(x => new Skill
            {
                SkillName = x.SkillName,
                SkillId = x.SkillId,
                Status = x.Status,
                
            }).ToList();
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;

            return cms;
        }

        public CMS GetApplicationPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 4;
            CMS cms = new CMS();
            var application = _ciPlatformContext.MissionApplications.Where(x => x.ApprovalStatus == "Pending" && x.DeletedAt == null)
                .Include(x => x.Mission).Include(x => x.User).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                application = application.Where(x =>x.Mission.Title.ToLower().Contains(Search.ToLower()) ||
                x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)application.Count / pageSize);
            List<MissionApplication> pagedUsers = application
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            cms.missionApplication = pagedUsers.Select(x => new MissionApplication()
            {
                Mission = x.Mission,
                User = x.User,
                AppliedAt = x.AppliedAt,
                MissionApplicationId = x.MissionApplicationId,
            }).ToList();
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;
            return cms;
        }

        public CMS GetThemePages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 4;
            CMS cms = new CMS();
            var theme = _ciPlatformContext.MissionThemes.Where(x => x.DeletedAt == null).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                theme = theme.Where(x =>  
                x.Title.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)theme.Count / pageSize);
            List<MissionTheme> pagedUsers = theme
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            cms.MissionTheme = pagedUsers.Select(x => new MissionTheme()
            {
                Status = x.Status,
                MissionThemeId = x.MissionThemeId,
                Title = x.Title,
            }).ToList();
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;
            return cms;
        }


        public void AddCmsData(string Title, string Description, string Slug, string Status, string demo, long cmsId)
        {
            if(demo == "add" && Title != null && Description != null && Slug != null && Status != null)
            {
                CmsPage cmsPage = new CmsPage();
                cmsPage.Title = Title;
                cmsPage.Description = Description;
                cmsPage.Slug = Slug;
                if (Status != null && Status == "True")
                {
                    cmsPage.Status = true;
                }
                else
                {
                    cmsPage.Status = false;
                }
                _ciPlatformContext.CmsPages.Add(cmsPage);
                _ciPlatformContext.SaveChanges();
            }
            else if(demo =="update")
            {
                CmsPage cmsPage = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsId).FirstOrDefault();
                cmsPage.Title = Title;
                cmsPage.Description = Description;
                cmsPage.Slug = Slug;
                if (Status != null && Status == "True")
                {
                    cmsPage.Status = true;
                }
                else
                {
                    cmsPage.Status = false;
                }
                _ciPlatformContext.CmsPages.Update(cmsPage);
                _ciPlatformContext.SaveChanges();
            }
        }

        public bool AddSkill(CMS cms)
        {
            Skill skill1 = _ciPlatformContext.Skills.Where(x => x.SkillName.ToLower().Contains(cms.skill.SkillName.ToLower())).FirstOrDefault();
            if(skill1 == null)
            {
                Skill skill = new Skill();
                skill = cms.skill;
                _ciPlatformContext.Skills.Add(skill);
                _ciPlatformContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddTheme(CMS cms)
        {
            MissionTheme missionTheme = _ciPlatformContext.MissionThemes.Where(x => x.Title.ToLower().Contains(cms.missionTheme.Title.ToLower())).FirstOrDefault();
            if(missionTheme == null)
            {
                MissionTheme theme = new MissionTheme();
                theme = cms.missionTheme;
                _ciPlatformContext.MissionThemes.Add(theme);
                _ciPlatformContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddUserData(CMS cms)
        {
            if(cms != null)
            {
                User user = new User();
                user = cms.User;
                _ciPlatformContext.Users.Add(user);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void UpdateUserData(CMS cms)
        {
            
            User user = _ciPlatformContext.Users.Where(x => x.UserId == cms.User.UserId).FirstOrDefault();               
            user.FirstName = cms.User.FirstName;
            user.LastName = cms.User.LastName;
            user.Email = cms.User.Email;
            user.Password = cms.User.Password;
            user.WhyIVolunteer = cms.User.WhyIVolunteer;
            user.Department = cms.User.Department;
            user.Status = cms.User.Status;
            user.CountryId = cms.User.CountryId;
            user.CityId = cms.User.CityId;
            user.EmployeeId = cms.User.EmployeeId;
            _ciPlatformContext.Users.Update(user);
            _ciPlatformContext.SaveChanges();
        }

        public void DeteleUserData(long userId)
        {
            if(userId != null && userId != 0)
            {
                User user = _ciPlatformContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
                user.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(user);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void UpdateSkillData(CMS cms)
        {
            Skill skill = _ciPlatformContext.Skills.Where(x => x.SkillId == cms.skill.SkillId).FirstOrDefault();
            skill.SkillName = cms.skill.SkillName;
            skill.Status = cms.skill.Status;
            skill.SkillId = cms.skill.SkillId;
            _ciPlatformContext.Skills.Update(skill);
            _ciPlatformContext.SaveChanges();
        }

        public void UpdateThemeData(CMS cms)
        {
            MissionTheme missionTheme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == cms.missionTheme.MissionThemeId).FirstOrDefault();
            missionTheme.Title = cms.missionTheme.Title;
            missionTheme.Status = cms.missionTheme.Status;
            missionTheme.MissionThemeId = cms.missionTheme.MissionThemeId;
            _ciPlatformContext.MissionThemes.Update(missionTheme);
            _ciPlatformContext.SaveChanges();
        }

        public void approveStory(long storyId, string approveStoryTitle)
        {
            if(storyId != null && storyId != 0)
            {
                Story story = _ciPlatformContext.Stories.Where(x => x.StoryId == storyId).FirstOrDefault();
                story.Status = "Approved";
                _ciPlatformContext.Update(story);
                _ciPlatformContext.SaveChanges();

                int userId = (int)story.UserId;
                Notification notification = new Notification();
                notification.NotificationMessage = "Your story " + approveStoryTitle + " has been approved";
                notification.UserId = userId;
                notification.StoryId = storyId;
                notification.NotificationType = "Story";
                _ciPlatformContext.Add(notification);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void rejectStory(long storyId, string rejectStoryTitle)
        {
            if(storyId != null && storyId != 0)
            {
                Story story = _ciPlatformContext.Stories.Where(x => x.StoryId == storyId).First();
                story.Status = "Declined";
                _ciPlatformContext.Update(story);
                _ciPlatformContext.SaveChanges();

                int userId = (int)story.UserId;
                Notification notification = new Notification();
                notification.NotificationMessage = "Your story " + rejectStoryTitle + " has been rejected";
                notification.UserId=userId;
                notification.StoryId = storyId;
                notification.NotificationType = "Story";
                _ciPlatformContext.Notifications.Add(notification);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteStory(long storyId, string deleteStoryTitle)
        {
            if(storyId != null && storyId != 0)
            {
                Story story = _ciPlatformContext.Stories.Where(x => x.StoryId == storyId).First();
                story.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(story);
                _ciPlatformContext.SaveChanges();

                int userId = (int)story.UserId;
                Notification notification = new Notification();
                notification.NotificationMessage = "Your story " + deleteStoryTitle + " has been deleted";
                notification.UserId = userId;
                notification.StoryId = storyId;
                notification.NotificationType = "Story";
                _ciPlatformContext.Notifications.Add(notification);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void approveApplication(long applicationId)
        {
            if(applicationId != null && applicationId != 0)
            {
                MissionApplication missionApplication = _ciPlatformContext.MissionApplications.Where(x => x.MissionApplicationId == applicationId).First();
                missionApplication.ApprovalStatus = "Approved";
                Mission mission = _ciPlatformContext.Missions.Where(x => x.MissionId == missionApplication.MissionId).FirstOrDefault();
                mission.TotalSeats = mission.TotalSeats - 1;
                _ciPlatformContext.Update(mission);
                _ciPlatformContext.Update(missionApplication);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void rejectApplication(long applicationId)
        {
            if(applicationId != null && applicationId != 0)
            {
                MissionApplication missionApplication = _ciPlatformContext.MissionApplications.Where(x => x.MissionApplicationId == applicationId).First();
                missionApplication.ApprovalStatus = "Declined";
                _ciPlatformContext.Update(missionApplication);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteApplication(long applicationId)
        {
            if(applicationId != null && applicationId != 0)
            {
                MissionApplication missionApplication = _ciPlatformContext.MissionApplications.Where(x => x.MissionApplicationId == applicationId).First();
                missionApplication.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(missionApplication);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteMission(long missionId)
        {
            if(missionId != null && missionId != 0)
            {
                Mission mission = _ciPlatformContext.Missions.Where(x => x.MissionId == missionId).First();
                mission.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(mission);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteSkill(long skillId)
        {
            if(skillId != null && skillId != 0)
            {
                Skill skill = _ciPlatformContext.Skills.Where(x => x.SkillId == skillId).First();
                skill.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(skill);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteTheme(long themeId)
        {
            if(themeId != null && themeId != 0)
            {
                MissionTheme missionTheme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == themeId).First();
                missionTheme.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(missionTheme);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteBanner(long bannerId)
        {
            if(bannerId != null && bannerId != 0)
            {
                Banner banner = _ciPlatformContext.Banners.Where(x => x.BannerId == bannerId).First();
                banner.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(banner);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void DeleteCmsPage(long cmsId)
        {
            if(cmsId != null && cmsId != 0)
            {
                CmsPage cmsPage = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsId).FirstOrDefault();
                cmsPage.DeletedAt = DateTime.Now;
                _ciPlatformContext.CmsPages.Update(cmsPage);
                _ciPlatformContext.SaveChanges();
            }
        }

        public CMS GetCmsData(long cmsPageId)
        {
            CMS cms = new CMS();
            CmsPage cmsPage = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsPageId).First();
            cms.CmsPage = cmsPage;

            return cms;
        }

        public CMS GetUserData(long UserId)
        {
            CMS cms = new CMS();
            User user = _ciPlatformContext.Users.Where(user => user.UserId == UserId).FirstOrDefault();
            cms.User = user;

            return cms;
        }

        public MissionCrud GetMissionData(long MissionId)
        {
            MissionCrud missionCrud = new MissionCrud();
            Mission mission = _ciPlatformContext.Missions.Where(x => x.MissionId == MissionId).FirstOrDefault();
            missionCrud.MissionId = MissionId;
            missionCrud.OrganizationName = mission.OrganizationName;
            missionCrud.OrganizationDetail = mission.OrganizationDetail;
            missionCrud.ShortDescription = mission.ShortDescription;
            missionCrud.StartDate = mission.StartDate;
            missionCrud.Status = mission.Status;
            missionCrud.ThemeId = mission.ThemeId;
            missionCrud.EndDate = mission.EndDate;
            missionCrud.Deadline = mission.Deadline;
            missionCrud.Description = mission.Description;
            missionCrud.CityId = mission.CityId;
            missionCrud.CountryId = mission.CountryId;
            missionCrud.Availability = mission.Availability;
            missionCrud.TotalSeats = mission.TotalSeats;
            missionCrud.MissionType = mission.MissionType;
            missionCrud.Title = mission.Title;

            return missionCrud;
        }

        

        public CMS GetSkillData(long SkillId)
        {
            CMS cms = new CMS();
            Skill skill = _ciPlatformContext.Skills.Where(x => x.SkillId == SkillId).First();
            cms.skill = skill;

            return cms;
        }

        public CMS GetThemeData(long missionThemeId)
        {
            CMS cms = new CMS();
            MissionTheme missionTheme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == missionThemeId).FirstOrDefault();
            cms.missionTheme = missionTheme;

            return cms;
        }

        public void addBanner(Banner banner)
        {
            _ciPlatformContext.Add(banner);
            _ciPlatformContext.SaveChanges();
        }



        public List<City> GetCitiesOfCountry(long country)
        {
            List<City> city = _ciPlatformContext.Cities.Where(x => x.Country.CountryId == country).ToList();
            return city;
        }

        public List<SelectListItem> GetSkills()
        {

            return _ciPlatformContext.Skills.Where(x => x.DeletedAt == null && x.Status == true).Select(x => new SelectListItem { Value = x.SkillId.ToString(), Text = x.SkillName }).ToList();
        }

        public List<SelectListItem> GetCountries()
        {
            return _ciPlatformContext.Countries.Select(x => new SelectListItem { Value = x.CountryId.ToString(), Text = x.Name }).ToList();
        }

        public List<SelectListItem> GetThemes()
        {
            return _ciPlatformContext.MissionThemes.Where(x => x.DeletedAt == null && x.Status == true).Select(x => new SelectListItem { Value = x.MissionThemeId.ToString(), Text = x.Title }).ToList();
        }

        public List<SelectListItem> GetSelectedSkills(long MissionId)
        {
            return _ciPlatformContext.MissionSkills.Where(us => us.MissionId == MissionId && us.Skill.DeletedAt == null).Join(_ciPlatformContext.Skills.Where(x => x.Status == true && x.DeletedAt == null), us => us.SkillId, s => s.SkillId, (us, s) => new SelectListItem { Value = s.SkillId.ToString(), Text = s.SkillName }).ToList();
        }

        public List<SelectListItem> GetNotSelectedSkills(long MissionId)
        {
            return _ciPlatformContext.Skills.Where(s => !_ciPlatformContext.MissionSkills.Where(us => us.MissionId == MissionId && us.Skill.DeletedAt == null && us.Skill.Status == true).Select(us => us.SkillId).Contains(s.SkillId) && s.DeletedAt == null && s.Status == true).Select(s => new SelectListItem { Value = s.SkillId.ToString(), Text = s.SkillName }).ToList();
        }

        public long AddMission(MissionCrud model)
        {
            Mission mission = new Mission();
            mission.Title = model.Title;
            mission.MissionType = model.MissionType;
            mission.Description = model.Description;
            mission.CountryId = model.CountryId;
            mission.ThemeId = model.ThemeId;
            mission.CityId = model.CityId;
            mission.CountryId = model.CountryId;
            mission.StartDate = model.StartDate;
            mission.EndDate = model.EndDate;
            mission.Deadline = model.Deadline;
            mission.Status = model.Status;
            mission.OrganizationDetail = model.OrganizationDetail;
            mission.OrganizationName = model.OrganizationName;
            mission.TotalSeats = model.TotalSeats;
            mission.Availability = model.Availability;
            mission.ShortDescription = model.ShortDescription;
            _ciPlatformContext.Missions.Add(mission);
            _ciPlatformContext.SaveChanges();
            if (model.SkillIDs != null)
            {
                string[] skillIDStrings = model.SkillIDs.Split(',');
                int[] skillIDs = Array.ConvertAll(skillIDStrings, int.Parse);
                List<MissionSkill> missionSkills = _ciPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId).ToList();
                List<MissionSkill> skillsToRemove = missionSkills.Where(x => !skillIDs.Contains(x.SkillId)).ToList();
                foreach (MissionSkill skill in skillsToRemove)
                {
                    _ciPlatformContext.MissionSkills.Remove(skill);
                    _ciPlatformContext.SaveChanges();
                }
                foreach (int skillID in skillIDs)
                {
                    MissionSkill missionSkill = _ciPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId && x.SkillId == skillID).FirstOrDefault();
                    if (missionSkill == null)
                    {
                        missionSkill = new MissionSkill();
                        missionSkill.MissionId = mission.MissionId;
                        missionSkill.SkillId = skillID;
                        _ciPlatformContext.MissionSkills.Add(missionSkill);
                        _ciPlatformContext.SaveChanges();
                    }
                }
            }
            else
            {
                List<MissionSkill> missionSkills = _ciPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId).ToList();
                foreach (MissionSkill skill in missionSkills)
                {
                    _ciPlatformContext.MissionSkills.Remove(skill);
                    _ciPlatformContext.SaveChanges();
                }
            }
            return mission.MissionId;
        }

        public long EditMission(MissionCrud model)
        {
            Mission mission = _ciPlatformContext.Missions.Where(x => x.MissionId == model.MissionId).FirstOrDefault();
            mission.Title = model.Title;
            mission.MissionType = model.MissionType;
            mission.Description = model.Description;
            mission.CountryId = model.CountryId;
            mission.ThemeId = model.ThemeId;
            mission.CityId = model.CityId;
            mission.CountryId = model.CountryId;
            mission.StartDate = model.StartDate;
            mission.EndDate = model.EndDate;
            mission.Deadline = model.Deadline;
            mission.Status = model.Status;
            mission.OrganizationDetail = model.OrganizationDetail;
            mission.OrganizationName = model.OrganizationName;
            mission.TotalSeats = model.TotalSeats;
            mission.Availability = model.Availability;
            mission.ShortDescription = model.ShortDescription;
            _ciPlatformContext.Missions.Update(mission);
            _ciPlatformContext.SaveChanges();
            if (model.SkillIDs != null)
            {
                string[] skillIDStrings = model.SkillIDs.Split(',');
                int[] skillIDs = Array.ConvertAll(skillIDStrings, int.Parse);
                List<MissionSkill> missionSkills = _ciPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId).ToList();
                List<MissionSkill> skillsToRemove = missionSkills.Where(x => !skillIDs.Contains(x.SkillId)).ToList();
                foreach (MissionSkill skill in skillsToRemove)
                {
                    _ciPlatformContext.MissionSkills.Remove(skill);
                    _ciPlatformContext.SaveChanges();
                }
                foreach (int skillID in skillIDs)
                {
                    MissionSkill missionSkill = _ciPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId && x.SkillId == skillID).FirstOrDefault();
                    if (missionSkill == null)
                    {
                        missionSkill = new MissionSkill();
                        missionSkill.MissionId = mission.MissionId;
                        missionSkill.SkillId = skillID;
                        _ciPlatformContext.MissionSkills.Add(missionSkill);
                        _ciPlatformContext.SaveChanges();
                    }
                }
            }
            else
            {
                List<MissionSkill> missionSkills = _ciPlatformContext.MissionSkills.Where(x => x.MissionId == mission.MissionId).ToList();
                foreach (MissionSkill skill in missionSkills)
                {
                    _ciPlatformContext.MissionSkills.Remove(skill);
                    _ciPlatformContext.SaveChanges();
                }
            }
            return mission.MissionId;
        }

        public void RemoveMissionMedia(long missionId)
        {
            var missionMedia = _ciPlatformContext.MissionMedia.Where(mm => mm.MissionId == missionId);
            _ciPlatformContext.MissionMedia.RemoveRange(missionMedia);
            _ciPlatformContext.SaveChanges();
        }

        public void RemoveMissionDocument(long missionId)
        {
            var missionMedia = _ciPlatformContext.MissionDocuments.Where(mm => mm.MissionId == missionId);
            _ciPlatformContext.MissionDocuments.RemoveRange(missionMedia);
            _ciPlatformContext.SaveChanges();
        }

        public void AddMissionMedia(long missionId, string imagepath, string fileName, string fileExtension)
        {
            MissionMedium media = _ciPlatformContext.MissionMedia.Where(mm => mm.MissionId == missionId).FirstOrDefault();
            if (media == null)
            {
                MissionMedium missionMedium = new MissionMedium();
                missionMedium.MissionId = missionId;
                missionMedium.MediaPath = "/uploads/";
                missionMedium.MediaName = Path.GetFileNameWithoutExtension(fileName);
                missionMedium.MediaType = fileExtension.TrimStart('.');
                _ciPlatformContext.MissionMedia.Add(missionMedium);
            }
            else
            {
                media.MediaName = media.MediaName + "," + Path.GetFileNameWithoutExtension(fileName);
                _ciPlatformContext.MissionMedia.Update(media);
            }
            _ciPlatformContext.SaveChanges();
        }

        public void AddMissionDocument(long missionId, string imagepath, string fileName, string fileExtension)
        {
            MissionDocument missionDocument = new MissionDocument();
            missionDocument.MissionId = missionId;
            missionDocument.DocumentName = Path.GetFileNameWithoutExtension(fileName);
            missionDocument.DocumentType = fileExtension.TrimStart('.');
            missionDocument.DocumentPath = "/assets/";
            _ciPlatformContext.MissionDocuments.Add(missionDocument);
            _ciPlatformContext.SaveChanges();
        }
    }
}
