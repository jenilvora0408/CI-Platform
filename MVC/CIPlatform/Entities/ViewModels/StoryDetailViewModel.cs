using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class StoryDetailViewModel
    {
        public List<StoryMedium> storyMedia { get; set; }
        public User? user { get; set; }
        public Story? story { get; set; }
    }
}
