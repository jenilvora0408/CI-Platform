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

        public void AddCmsData(string Title, string Description, string Slug, string Status);
    }
}
