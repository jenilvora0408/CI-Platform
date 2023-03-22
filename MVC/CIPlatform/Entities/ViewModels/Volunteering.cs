using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public  class Volunteering
    {
        [Key]
        public long MissionId { get; set; }
        public string? cityName { get; set; }
        public string? themeName { get; set; }
        public string? missionTitle { get; set; }
        public string? missionShortDesc { get; set; }
        public string? organizationName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public long? availableSeats { get; set; }
        public string? goalObjective { get; set; }
        public int? goalValue { get; set; }
        public string? missionImage { get; set; }
        public string? mediaPath { get; set; }
        public string? mediaType { get; set; }
        //public string? MediaName { get; set; }
        public string? introduction { get; set; }
        public string? days { get; set; }
        public string? skill { get; set; }
        public string? organizationDetail { get; set; }
        public long? favMissionId { get; set; }
        public DateTime? deadline { get; set; }
        public int? Rating { get; set; }
        public int? ratingvolcount { get; set; }
        public string? approvalStatus { get; set; }
    }
}
