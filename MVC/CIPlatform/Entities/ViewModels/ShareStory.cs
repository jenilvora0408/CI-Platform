using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ShareStory
    {
        public Navbar_1? Navbar_1 { get; set; }
        
        public List<Mission> mission { get; set; }
        public List<MissionApplication> application { get; set; }
    }
}
