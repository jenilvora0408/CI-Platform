using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.ViewModels
{
    public class CMS
    {
        public Navbar_1? Navbar_1 { get; set; }

        public List<CmsPage> cmsPage { get; set; }

        public CmsPage CmsPage { get; set; }

        public List<User> user { get; set; }

        public User User { get; set; }

        public List<Story> stories { get; set; }

        public List<Mission> missions { get; set; }

        public List<MissionSkills> missionSkills { get; set;}

        public List<MissionApplication> missionApplication { get; set; }

        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
