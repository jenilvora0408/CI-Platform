using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.ViewModels
{
    public class Navbar_1
    {
     
        [Key]
        public long? userId { get; set; } = null;
        public string? username { get; set; }
        public string? avatar { get; set; }
        
        public List<Notification> notifications { get; set; } = new List<Notification>();
    }
}
