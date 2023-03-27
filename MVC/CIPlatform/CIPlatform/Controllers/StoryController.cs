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
        private readonly IWebHostEnvironment _env;

        public StoryController(ILogger<AccountController> logger, MissionInterface missionInterface, CiPlatformContext ciPlatformContext, IWebHostEnvironment env)
        {
            _logger = logger;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _env = env;

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
           IEnumerable<MissionApplication> missionApplications = _ciPlatformContext.MissionApplications.Where(x => x.UserId == userObj.UserId && x.ApprovalStatus == "Approved").Include(x => x.Mission).AsEnumerable();
            return Json(new { data = missionApplications });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(List<IFormFile> files, long StoryId)
        {
            foreach (var file in files)
            {
                // Save the file to the desired folder
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                StoryMedium storymedia = new StoryMedium();
                storymedia.Path = "/uploads/" + fileName;
                storymedia.Type = file.ContentType.Substring(6,9);
                
                storymedia.StoryId = StoryId;

                _ciPlatformContext.StoryMedia.Add(storymedia);
                _ciPlatformContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult storyInsert(long DropdownItem,string TitleOfStory,string StoryDate,string EditorText)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            Story story = new Story();
            story.Title = TitleOfStory;
            story.MissionId = DropdownItem;
            story.CreatedAt  = DateTime.Now;
            story.Description = EditorText;
            story.UserId = userObj.UserId;
            _ciPlatformContext.Stories.Add(story);
            _ciPlatformContext.SaveChanges();
            return Ok(story.StoryId);
        }
    }
}
