using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class VolTimesheet
    {
        public string missionTitle { get; set; }

        public long? missionId { get; set; }

        [Range(0, 23, ErrorMessage = "Hours must be between 0 and 23.")]
        public int? hours { get; set; }

        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int? minutes { get; set; }

        public long? TimesheetId { get; set; }        

        public long? UserId { get; set; }

        public long? MissionId { get; set; }

        public TimeSpan? Time { get; set; }

        public int? Action { get; set; }

        public DateTime? DateVolunteered { get; set; }

        public string? Notes { get; set; }

    }
}
