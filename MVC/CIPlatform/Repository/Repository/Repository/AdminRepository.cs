using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
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
            var mission = _ciPlatformContext.Missions.ToList();
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
            var skill = _ciPlatformContext.MissionSkills.Include(x => x.Skill).Include(x => x.Mission).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                skill = skill.Where(x => x.Skill.SkillName.ToLower().Contains(Search.ToLower()) || 
                x.Mission.Title.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)skill.Count / pageSize);
            List<MissionSkills> pagedUsers = skill
                .Skip((pageNumber - 1) *pageSize)
                .Take(pageSize)
                .ToList();
            cms.missionSkills = pagedUsers.Select(x => new MissionSkills()
            {
                Mission = x.Mission,
                Skill = x.Skill
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
            var theme = _ciPlatformContext.Missions.Include(x => x.Theme).ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                theme = theme.Where(x => x.Title.ToLower().Contains(Search.ToLower()) || 
                x.Theme.Title.ToLower().Contains(Search.ToLower()) ).ToList();
            }
            int totalCount = (int)Math.Ceiling((double)theme.Count / pageSize);
            List<Mission> pagedUsers = theme
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            cms.missions = pagedUsers.Select(x => new Mission()
            {
                Title = x.Title,
                Theme = x.Theme
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

        public void approveStory(long storyId)
        {
            if(storyId != null && storyId != 0)
            {
                Story story = _ciPlatformContext.Stories.Where(x => x.StoryId == storyId).FirstOrDefault();
                story.Status = "Approved";
                _ciPlatformContext.Update(story);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void rejectStory(long storyId)
        {
            if(storyId != null && storyId != 0)
            {
                Story story = _ciPlatformContext.Stories.Where(x => x.StoryId == storyId).First();
                story.Status = "Declined";
                _ciPlatformContext.Update(story);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void deleteStory(long storyId)
        {
            if(storyId != null && storyId != 0)
            {
                Story story = _ciPlatformContext.Stories.Where(x => x.StoryId == storyId).First();
                story.DeletedAt = DateTime.Now;
                _ciPlatformContext.Update(story);
                _ciPlatformContext.SaveChanges();
            }
        }

        public void approveApplication(long applicationId)
        {
            if(applicationId != null && applicationId != 0)
            {
                MissionApplication missionApplication = _ciPlatformContext.MissionApplications.Where(x => x.MissionApplicationId == applicationId).First();
                missionApplication.ApprovalStatus = "Approved";
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
    }
}
