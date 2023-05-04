using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
