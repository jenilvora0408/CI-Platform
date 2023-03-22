using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class MissionList
    {
        [Key]
        public long MissionId { get; set; } 
        public string? cityName { get; set; }
        public string? themeName { get; set; }
        public string?  missionTitle { get; set; }
        public string? missionShortDesc { get; set; }
        public string? organizationName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public long? availableSeats { get; set; }
        public string? goalObjective { get; set; }
        public string? missionImage { get; set; }
        public string? mediaPath { get; set; }
        public string? mediaType { get; set; }
        public decimal? Rating { get; set; }
        public int? goalValue { get; set; }
        public DateTime? deadline { get; set; }
        public long? favMissionId { get; set; }


    }
}
