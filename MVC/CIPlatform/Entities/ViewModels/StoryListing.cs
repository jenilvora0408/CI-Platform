using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class StoryListing
    {
        [Key]
        public long? storyID { get; set; }
        public long? storymissionID { get; set; }
        public long? storyuserID { get; set; }
        public string storyDescription { get; set; }
        public string storyTitle { get; set; }
        public string storyApprovalStatus { get; set; }
        public string storyImagePath { get; set; }
        public string storyImageType { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Avatar { get; set; }
        public string missionTheme { get; set; }
    }
}
