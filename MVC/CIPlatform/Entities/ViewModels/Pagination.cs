using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class Pagination
    {
        public long? pageCount { get; set; }
        public long? activePage { get; set; }
        public long? pageSize { get; set; }
        public List<MissionList> missions { get; set; }
    }
}
