using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface AdminInterface
    {
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

        public void approveStory(long storyId);

        public void rejectStory(long storyId);

        public void deleteStory(long storyId);
    }
}
