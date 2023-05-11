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
using CIPlatform.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CIPlatform.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly StoryInterface _storyInterface;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public StoryController(ILogger<AccountController> logger, MissionInterface missionInterface, CiPlatformContext ciPlatformContext, 
            IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, StoryInterface storyInterface,
            IHubContext<NotificationHub> notificationHub)
        {
            _logger = logger;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _storyInterface = storyInterface;
            _notificationHub = notificationHub;
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




        public IActionResult StoryDetail(long? storyID)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            storyDetail storyDetail = new storyDetail();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            storyDetail.Navbar_1 = missionHomeModel;
            storyDetail.StoryId = storyID;
            Story stories = _storyInterface.GetStoryById(storyID.Value);
            stories.Views = stories.Views + 1;
            User user = _storyInterface.GetUserById(stories.UserId);
            storyDetail.Avatar = user.Avatar;
            storyDetail.WhyIVolunteer = user.WhyIVolunteer;
            storyDetail.Name = user.FirstName + " " + user.LastName;
            StoryMedium media = _storyInterface.GetStoryMediaByStoryId(storyID.Value);
            storyDetail.StoryDescription = stories.Description;
            storyDetail.StoryTitle = stories.Title;
            storyDetail.Views = stories.Views;
            storyDetail.MissionId = stories.MissionId;
            storyDetail.mediaPath = media.Path;
            storyDetail.mediaType = media.Type;
            _storyInterface.UpdateStory(stories);

            return View(storyDetail);
        }

        [HttpPost]
        public IActionResult RecommandtoCoWorker(string InviteEmail, long StoryId)
        {

            string email = HttpContext.Session.GetString("useremail");
            var userfrom = _missionInterface.findUser((string)email);
            var userto = _missionInterface.findUser((string)InviteEmail);

            var userFromName = userfrom.FirstName + " " + userfrom.LastName;
            var userToName = userto.FirstName + " " + userto.LastName;
            var userToId = userto.UserId;
            var storyTitle = _storyInterface.FindStoryTitle(StoryId);

            bool shareMessage = _storyInterface.RecommandtoCoWorker((long)userfrom.UserId, (int)StoryId, (long)userto.UserId);
            string message = "";
            if (shareMessage)
            {
                message = "You Alerady Invited";
                ViewBag.Message = "You Alerady Invited";
            }
            else
            {
                string mailmessage = "Welcome to CI Platform, <br/> your friend " + userfrom.FirstName + " " + userfrom.LastName + " " + "You can View Story Using Below link.. </br>";
                string path = "<a href=\"" + "https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Story/StoryDetail?storyId=" + StoryId + "\"style=\"font-weight:500;color:blue;\">Invite to Story</a>";
                MailHelper mailHelper = new MailHelper(_configuration);
                ViewBag.sendMail = mailHelper.Send(InviteEmail, mailmessage + path);
                message = "You Successfully Invited";
                ViewBag.Message = "You Successfully Invited";
                ModelState.Clear();
            }
            _notificationHub.Clients.User(userToName).SendAsync("ReceiveMsg", "Your friend " + userFromName + " has invited you to view story - " + storyTitle.Title);
            _missionInterface.addNotificationForRecommendMission(StoryId, userToId, userFromName, storyTitle.Title);
            return Json(new { data = message });
        }

        public IActionResult StoryCard(int? pageNumber)
        {
            StoryPage storyPage = _storyInterface.GetStoryListings(pageNumber ?? 1);
            return PartialView("_StoryListingCard", storyPage);
        }


        public IActionResult ShareStory()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ShareStory shareStory = new ShareStory();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            shareStory.Navbar_1 = missionHomeModel;
            return View(shareStory);
        }


        public IActionResult userAplliedMission()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            IEnumerable<MissionApplication> missionApplications = _storyInterface.GetApprovedMissionApplicationsForUser((int)userObj.UserId);
            return Json(new { data = missionApplications });
        }


        [HttpPost]
        public async Task<IActionResult> UploadImages([FromForm] ImageUploadViewModel imageUploadViewModel)
        {
            await _storyInterface.UploadImages(imageUploadViewModel);
            return Ok();
        }

        [HttpPost]
        public IActionResult storyInsert(long DropdownItem, string TitleOfStory, string StoryDate, string EditorText)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);

            var storyId = _storyInterface.InsertOrUpdateStory(DropdownItem, TitleOfStory, EditorText, userObj.UserId);

            return Ok(storyId);
        }


        [HttpPost]
        public IActionResult submitStory(long DropdownItem, string TitleOfStory, string StoryDate, string EditorText)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);

            var story = _storyInterface.SubmitStory(userObj.UserId, DropdownItem, TitleOfStory, EditorText);

            return Ok(story.StoryId);
        }

        public IActionResult SearchUser(string name)
        {
            List<User> user = _storyInterface.SearchUsers(name);
            return PartialView("_RecommendUser", user);
        }
    }
}
