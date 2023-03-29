using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class storyDetail
    {
        
        public Navbar_1? Navbar_1 { get; set; }
        public long? StoryId { get; set; }
        public string StoryTitle { get; set; }
        public string StoryDescription { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string? WhyIVolunteer { get; set; }
        public long? Views { get; set; }
        public long MissionId { get; set; }
        public string? ToEmail { get; set; }

     
        public string? mediaPath { get; set; }
        public string? mediaType { get; set; }
    }
}
