using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ImageUploadViewModel
    {
        public List<IFormFile>? files { get; set; }

        public long StoryId { get; set; }

        public string? storyUrl { get; set; }
    }
}
