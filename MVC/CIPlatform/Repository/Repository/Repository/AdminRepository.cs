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
                cmsPages = _ciPlatformContext.CmsPages.Where(x => x.Title.ToLower().Contains(Search.ToLower()) ).ToList();
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
