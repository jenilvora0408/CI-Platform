using Entities.ViewModels;
using Entities.Data;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Interface;
using System.Diagnostics;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RegisterInterface _registerInterface;

        public HomeController(ILogger<HomeController> logger, RegisterInterface registerInterface)
        {
            _logger = logger;
            _registerInterface = registerInterface;
        }

        public IActionResult Index()
        {
            List<User> lstUsers = _registerInterface.GetUsersList();
            return View(lstUsers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}