using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class MissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MissionListing()
        {
            return View();
        }

        public IActionResult MissionListing_List()
        {
            return View();
        }
    }
}
