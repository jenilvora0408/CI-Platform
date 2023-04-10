using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.ViewModels
{
    public class VolunteeringTimesheet
    {
        public Navbar_1? Navbar_1 { get; set; }
        public List<VolTimesheet> goalTimesheets { get; set; }
        public List<VolTimesheet> timesheets { get; set; }
        public VolTimesheet VolTimesheet { get; set; }
    }
}
