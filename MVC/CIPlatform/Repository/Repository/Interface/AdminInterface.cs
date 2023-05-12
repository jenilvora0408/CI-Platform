using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface AdminInterface
    {
        public CMS GetBannerPages(string Search, int pageNumber);

        public CMS GetCmsPages(string Search, int pageNumber);

        public CMS GetUserPages(string Search, int pageNumber);

        public CMS GetStoryPages(string Search, int pageNumber);

        public CMS GetMissionPages(string Search, int pageNumber);

        public CMS GetSkillPages(string Search, int pageNumber);

        public CMS GetApplicationPages(string Search, int pageNumber);

        public CMS GetThemePages(string Search, int pageNumber);

        public void AddCmsData(string Title, string Description, string Slug, string Status, string demo, long cmsId);

        public void AddUserData(CMS cms);

        public void UpdateUserData(CMS cms);

        public void DeteleUserData(long userId);

        public void DeleteCmsPage(long cmsId);

        public CMS GetCmsData(long cmsPageId);

        public CMS GetUserData(long UserId);

        public CMS GetSkillData(long SkillId);

        public string approveStory(long storyId, string approveStoryTitle);

        public string rejectStory(long storyId, string rejectStoryTitle);

        public string deleteStory(long storyId, string deleteStoryTitle);

        public string approveApplication(long applicationId, string approveTitle, long approveMissionId);
        
        public string rejectApplication(long applicationId, string rejectTitle, long rejectMissionId);

        public string deleteApplication(long applicationId, string deleteTitle, long deleteMissionId);

        public bool AddSkill(CMS cms);

        public void UpdateSkillData(CMS cms);

        public void deleteSkill(long skillId);

        public bool AddTheme(CMS cms);

        public CMS GetThemeData(long missionThemeId);

        public void UpdateThemeData(CMS cms);

        public void deleteTheme(long themeId);

        public void addBanner(Banner banner);

        public void deleteBanner(long bannerId);

        public void deleteMission(long missionId);

        public List<City> GetCitiesOfCountry(long country);

        public long AddMission(MissionCrud model);

        public void AddMissionMedia(long missionId, string imagepath, string fileName, string fileExtension);

        public void AddMissionDocument(long missionId, string imagepath, string fileName, string fileExtension);

        public MissionCrud GetMissionData(long MissionId);

        public long EditMission(MissionCrud model);

        public void RemoveMissionDocument(long missionId);

        public void RemoveMissionMedia(long missionId);

        public List<SelectListItem> GetSkills();

        public List<SelectListItem> GetCountries();

        public List<SelectListItem> GetThemes();

        public List<SelectListItem> GetSelectedSkills(long MissionId);

        public List<SelectListItem> GetNotSelectedSkills(long MissionId);
    }
}
