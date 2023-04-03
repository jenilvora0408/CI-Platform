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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly StoryInterface _storyInterface;

        public StoryController(ILogger<AccountController> logger, MissionInterface missionInterface, CiPlatformContext ciPlatformContext, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, StoryInterface storyInterface)
        {
            _logger = logger;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _storyInterface = storyInterface;
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
                //string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Mission/MissionVolunteering?token=" + MissionId + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                MailHelper mailHelper = new MailHelper(_configuration);
                ViewBag.sendMail = mailHelper.Send(InviteEmail, mailmessage + path);
                message = "You Successfully Invited";
                ViewBag.Message = "You Successfully Invited";
                ModelState.Clear();
            }
            return Json(new { data = message });
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
        public async Task<IActionResult> UploadImages([FromForm] ImageUploadViewModel imageUploadViewModel)
        {
            string imgName = "";
            if (imageUploadViewModel.storyUrl != null)
            {
                StoryMedium storymedia = new StoryMedium();
                storymedia.Path = imageUploadViewModel.storyUrl;
                storymedia.Type = "URL";
                storymedia.StoryId = imageUploadViewModel.StoryId;
                _ciPlatformContext.StoryMedia.Add(storymedia);
                _ciPlatformContext.SaveChangesAsync();
            }
            foreach (var file in imageUploadViewModel.files)
            {
                
                    // Save the file to the desired folder
                    var fileName = Path.GetFileName(file.FileName);
                imgName += "/uploads/" + fileName + ",";
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                if (!_ciPlatformContext.StoryMedia.Any(u => u.StoryId == imageUploadViewModel.StoryId))
                {
                    StoryMedium storymedia = new StoryMedium();
                    storymedia.Path = "/uploads/" + fileName;
                    int index = file.ContentType.IndexOf("/");
                    string textAfterSlash = file.ContentType.Substring(index + 1);
                    storymedia.Type = textAfterSlash;
                    storymedia.StoryId = imageUploadViewModel.StoryId;

                    _ciPlatformContext.StoryMedia.Add(storymedia);
                    await _ciPlatformContext.SaveChangesAsync();
                }
                else
                {
                    StoryMedium storymedia = _ciPlatformContext.StoryMedia.Where(u => u.StoryId == imageUploadViewModel.StoryId).First();
                    storymedia.Path = imgName;
                    int index = file.ContentType.IndexOf("/");
                    string textAfterSlash = file.ContentType.Substring(index + 1);
                    storymedia.Type = textAfterSlash;
                    storymedia.StoryId = imageUploadViewModel.StoryId;

                    _ciPlatformContext.StoryMedia.Update(storymedia);
                    await _ciPlatformContext.SaveChangesAsync();
                }
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult storyInsert(long DropdownItem,string TitleOfStory,string StoryDate,string EditorText)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (!_ciPlatformContext.Stories.Any(u => u.UserId == userObj.UserId && u.MissionId == DropdownItem && u.Title == TitleOfStory))
            {
                Story story = new Story();
                story.Title = TitleOfStory;
                story.MissionId = DropdownItem;
                story.CreatedAt = DateTime.Now;
                story.Description = EditorText;
                story.UserId = userObj.UserId;
                _ciPlatformContext.Stories.Add(story);
                _ciPlatformContext.SaveChanges();
                return Ok(story.StoryId);
            }
            else
            {
                Story story = _ciPlatformContext.Stories.Where(u => u.UserId == userObj.UserId && u.MissionId == DropdownItem && u.Title == TitleOfStory).First();
                story.Title = TitleOfStory;
                story.MissionId = DropdownItem;
                story.CreatedAt = DateTime.Now;
                story.Description = EditorText;
                story.UserId = userObj.UserId;
                _ciPlatformContext.Stories.Update(story);
                _ciPlatformContext.SaveChanges();
                return Ok(story.StoryId);
            }
        }

        [HttpPost]
        public IActionResult submitStory(long DropdownItem, string TitleOfStory, string StoryDate, string EditorText)
        {
            
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (!_ciPlatformContext.Stories.Any(u => u.UserId == userObj.UserId && u.MissionId == DropdownItem && u.Title == TitleOfStory))
            {
                Story story = new Story();
                story.Title = TitleOfStory;
                story.MissionId = DropdownItem;
                story.CreatedAt = DateTime.Now;
                story.Status = "Pending";
                story.Description = EditorText;
                story.UserId = userObj.UserId;
                _ciPlatformContext.Stories.Add(story);
                _ciPlatformContext.SaveChanges();
                return Ok(story.StoryId);
            }
            else
            {
                Story story = _ciPlatformContext.Stories.Where(u => u.UserId == userObj.UserId && u.MissionId == DropdownItem && u.Title == TitleOfStory).First();
                story.Title = TitleOfStory;
                story.MissionId = DropdownItem;
                story.CreatedAt = DateTime.Now;
                story.Status = "Pending";
                story.Description = EditorText;
                story.UserId = userObj.UserId;
                _ciPlatformContext.Stories.Update(story);
                _ciPlatformContext.SaveChanges();
                return Ok(story.StoryId);
            }
        }
        public IActionResult SearchUser(string name)
        {
            List<User> user = _ciPlatformContext.Users.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name) || x.Email.Contains(name)).Take(10).ToList();
            return PartialView("_RecommendUser", user);
        }
    }
}
