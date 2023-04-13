using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Interface;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AdminInterface _adminInterface;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AdminController(ILogger<AccountController> logger, AdminInterface adminInterface, CiPlatformContext ciPlatformContext,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, MissionInterface missionInterface, IWebHostEnvironment env)
        {
            _logger = logger;
            _adminInterface = adminInterface;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult CMS()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            CMS cms = new CMS();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            cms.Navbar_1 = missionHomeModel;
            return View(cms);
        }

        public IActionResult CmsTable(string Search, int pageNumber)
        {
            var a = _adminInterface.GetCmsPages(Search, pageNumber);
            
            return PartialView("_CMSList", a);
        }

        public IActionResult userTable(string Search, int pageNumber)
        {
            var b = _adminInterface.GetUserPages(Search, pageNumber);
            return PartialView("_UserTable", b);
        }

        public IActionResult storyTable(string Search, int pageNumber)
        {
            var c = _adminInterface.GetStoryPages(Search,pageNumber);
            return PartialView("_StoryTable", c);
        }

        public IActionResult missionTable(string Search, int pageNumber)
        {
            var d = _adminInterface.GetMissionPages(Search,pageNumber);
            return PartialView("_MissionTable", d);
        }

        public IActionResult AddCms()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            CMS cms = new CMS();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            cms.Navbar_1 = missionHomeModel;
            return View(cms);
        }

        [HttpPost]
        public IActionResult AddCmsData(string Title, string Description, string Slug, string Status)
        {
            _adminInterface.AddCmsData(Title, Description, Slug, Status);
            return Ok();
        }

        public IActionResult User()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            CMS cms = new CMS();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            cms.Navbar_1 = missionHomeModel;
            return View(cms);
        }

        public IActionResult Story()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            CMS cms = new CMS();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            cms.Navbar_1 = missionHomeModel;
            return View(cms);
        }

        public IActionResult Mission()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            CMS cms = new CMS();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            cms.Navbar_1 = missionHomeModel;
            return View(cms);
        }
    }
}
