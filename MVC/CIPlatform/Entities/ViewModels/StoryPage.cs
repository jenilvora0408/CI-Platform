using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class StoryPage
    {
        public StoryPage()
        {
            this.Navbar_1 = new Navbar_1();
        }
        public List<StoryListing> Stories { get; set; }
        public Navbar_1? Navbar_1 { get; set; }
        public long? pageCount { get; set; }
        public long? activePage { get; set; }
        public long? pageSize { get; set; }
        public long totalMissionCount { get; set; }
        public long? userId { get; set; } = null;
        public string? username { get; set; }
        public string? avatar { get; set; }
    }
}
