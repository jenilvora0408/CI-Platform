using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class MissionList
    {
        public string countryName { get; set; }
        public string themeName { get; set; }
        public string  missionTitle { get; set; }
        public string missionShortDesc { get; set; }
        public string organizationName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string availableSeats { get; set; }
        public string goalObjective { get; set; }
        public string missionImage { get; set; }
    }
}
