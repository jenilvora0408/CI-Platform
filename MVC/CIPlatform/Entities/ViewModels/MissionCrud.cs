using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class MissionCrud
    {
        public string SkillIDs { get; set; }

        public Navbar_1? Navbar_1 { get; set; }

        public long MissionId { get; set; }

        public long ThemeId { get; set; }

        public long CityId { get; set; }

        public long CountryId { get; set; }

        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string MissionType { get; set; } = null!;

        public bool Status { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetail { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public long? TotalSeats { get; set; }

        public DateTime? Deadline { get; set; }

        public string? Availability { get; set; }
    }
}
