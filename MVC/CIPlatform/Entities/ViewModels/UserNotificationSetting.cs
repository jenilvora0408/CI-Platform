using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class UserNotificationSetting
    {
        public bool newMission { get; set; }
        public bool recommendstory { get; set; }
        public bool recommendMission { get; set; }
        public bool missionApplication { get; set; }
        public bool myStory { get; set; }
        public bool email { get; set; }
        
    }
}
