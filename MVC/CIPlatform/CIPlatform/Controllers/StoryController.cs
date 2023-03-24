using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Repository.Interface;
using CI_PLATFORM_MAIN_ENTITIES.Models.ViewModels;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;

        public StoryController(ILogger<AccountController> logger, MissionInterface missionInterface, CiPlatformContext ciPlatformContext)
        {
            _logger = logger;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StoryListing()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            StoryPage storyPage = new StoryPage();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            storyPage.Navbar_1 = missionHomeModel;
            return View(storyPage);
        }
        
        public IActionResult StoryCard(int? pageNumber)
        {
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            StoryPage storyPage = new StoryPage();
            storyPage.Stories  = _ciPlatformContext.StoryListings.FromSqlInterpolated($"exec StoryListing @pageNumber={pageNumber},  @TotalCount = {output} out").ToList();
            storyPage.pageSize = 3;
            storyPage.activePage = pageNumber;
            storyPage.pageCount = long.Parse(output.Value.ToString());
            return PartialView("_StoryListingCard", storyPage);
        }

        public IActionResult ShareStory()
        {
            return View();
        }
    }
}
