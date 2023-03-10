using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class Utilities
    {
        public string search { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string theme { get; set; }
        public string skill { get; set; }
        public int pageStart { get; set; }
        public int pageLength { get; set; }


        //public string? newest { get; set; }
        //public string? oldest { get; set; }
        //public int? lowestAvailableSeats { get; set; }
        //public int? highestAvailableSeats { get; set; }
    }
}
