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
        public string sortBy { get; set; }
        public int? pageNumber { get; set; }
    }
}
