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
            cmsPages = _ciPlatformContext.CmsPages.Where(x => x.Status == status).ToList();
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
            user = _ciPlatformContext.Users.ToList();
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

        public CMS GetStoryPages(string Search, int pageNumber)
        {
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            int pageSize = 3;
            CMS cms = new CMS();
            var story = new List<Story>();
            story = _ciPlatformContext.Stories.Where(x => x.Status == "Pending").Include(x => x.User).Include(x => x.Mission).ToList();
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
                User = x.User
            }).ToList();    
            cms.PageCount = totalCount;
            cms.PageSize = pageSize;
            cms.CurrentPage = pageNumber;

            return cms;
        }


        public void AddCmsData(string Title, string Description, string Slug, string Status)
        {
            CmsPage cmsPage = new CmsPage();
            cmsPage.Title = Title;
            cmsPage.Description = Description;
            cmsPage.Slug = Slug;
            if(Status != null && Status == "True")
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
    }
}
