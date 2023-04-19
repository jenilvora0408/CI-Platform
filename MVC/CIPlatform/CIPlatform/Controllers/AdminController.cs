using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Banner()
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

        public IActionResult BannerTable(string Search, int pageNumber)
        {
            var banner = _adminInterface.GetBannerPages(Search, pageNumber);
            return PartialView("_BannerTable", banner);
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

        public IActionResult skillTable(string Search, int pageNumber)
        {
            var e = _adminInterface.GetSkillPages(Search,pageNumber);
            return PartialView("_SkillTable", e);
        }

        public IActionResult applicationTable(string Search, int pageNumber)
        {
            var f = _adminInterface.GetApplicationPages(Search,pageNumber);
            return PartialView("_ApplicationTable", f);
        }

        public IActionResult themeTable(string Search, int pageNumber)
        {
            var g = _adminInterface.GetThemePages(Search,pageNumber);
            return PartialView("_ThemeTable", g);
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

        public IActionResult AddMission()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            MissionCrud missionCrud = new MissionCrud();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            missionCrud.Navbar_1 = missionHomeModel;
            var missionskills = _ciPlatformContext.Skills.Where(x => x.DeletedAt == null).Select(x => new SelectListItem { Value = x.SkillId.ToString(), Text = x.SkillName }).ToList();
            ViewBag.missionskills = missionskills;
            var country = _ciPlatformContext.Countries.Select(x => new SelectListItem { Value = x.CountryId.ToString(), Text = x.Name }).ToList();
            ViewBag.Country = country;
            var theme = _ciPlatformContext.MissionThemes.Where(x => x.DeletedAt == null).Select(x => new SelectListItem { Value = x.MissionThemeId.ToString(), Text = x.Title }).ToList();
            ViewBag.theme = theme;
            return View(missionCrud);
        }

        [HttpPost]
        public async Task<IActionResult> AddMission(MissionCrud model, IEnumerable<IFormFile> fileImg, IEnumerable<IFormFile> fileDoc)
        {
            if (ModelState.IsValid)
            {
                var missionId = _adminInterface.AddMission(model);
                if (fileImg != null && fileImg.Count() > 0)
                {
                    string uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
                    foreach (var file in fileImg)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string fileExtension = Path.GetExtension(file.FileName);
                        if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                        {
                            fileName = fileName + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            string filePath = Path.Combine(uploadsFolderPath, fileName);
                            using (var fileImgtream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileImgtream);
                            }
                            string imagepath = "/uploads/" + fileName;
                            
                            _adminInterface.AddMissionMedia(missionId, imagepath, fileName, fileExtension);
                        }
                        else
                        {
                            ModelState.AddModelError("Image", "Please select png, jpg or jpeg file to upload.");
                        }
                    }
                    if (fileDoc != null && fileDoc.Count() > 0)
                    {
                        foreach (var file in fileDoc)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string fileExtension = Path.GetExtension(file.FileName);
                            if (fileExtension == ".pdf" || fileExtension == ".doc" || fileExtension == ".xlsx")
                            {
                                fileName = fileName + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                string filePath = Path.Combine(uploadsFolderPath, fileName);
                                using (var fileImgtream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileImgtream);
                                }
                                string imagepath = "/uploads/" + fileName;
                                _adminInterface.AddMissionDocument(missionId, imagepath, fileName, fileExtension);
                            }
                            else
                            {
                                ModelState.AddModelError("Image", "Please select png, jpg or jpeg file to upload.");
                            }
                        }
                    }
                    return RedirectToAction("Mission");
                }
            }
            return View(model);

        }

        public JsonResult GetCitiesOfCountry(long country)
        {
            var cities = _adminInterface.GetCitiesOfCountry(country);
            return Json(cities);
        }



        public IActionResult AddBanner()
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
        public IActionResult AddBanner(CMS adminBannerModel, IFormFile Image)
        {
          
                Banner banner = new Banner();

                string wwwRootPath = _env.WebRootPath;
                if (Image != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"banners");
                    var extension = Path.GetExtension(Image.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        Image.CopyTo(fileStreams);
                    }
                    banner.Image = @"\banners\" + fileName + extension;
                }
           
                banner.Text = adminBannerModel.banner.Text;
                banner.SortOrder = adminBannerModel.banner.SortOrder;
                _adminInterface.addBanner(banner);
                return RedirectToAction("Banner");
           
        }
        public IActionResult AddUser()
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

        public IActionResult EditUser(long UserId)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var editUser = _adminInterface.GetUserData(UserId);
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            editUser.Navbar_1 = missionHomeModel;

            return View(editUser);
        }

        public IActionResult EditSkill(long SkillId)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var editSkill = _adminInterface.GetSkillData(SkillId);
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            editSkill.Navbar_1 = missionHomeModel;

            return View(editSkill);
        }

        public IActionResult EditTheme(long missionThemeId)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var editTheme = _adminInterface.GetThemeData(missionThemeId);
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            editTheme.Navbar_1 = missionHomeModel;

            return View(editTheme);
        }

        public IActionResult EditCms(long cmsPageId)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var editCms = _adminInterface.GetCmsData(cmsPageId);
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            editCms.Navbar_1 = missionHomeModel;
            
            return View(editCms);
        }

        [HttpPost]
        public IActionResult AddCmsData(string Title, string Description, string Slug, string Status, string demo, long cmsId)
        {
            _adminInterface.AddCmsData(Title, Description, Slug, Status, demo, cmsId);
            return RedirectToAction("CMS", "Admin");
        }

        [HttpPost]
        public IActionResult AddUserData(CMS cms)
        {
            _adminInterface.AddUserData(cms);
            return RedirectToAction("User", "Admin");
        }

        [HttpPost]
        public IActionResult addSkill(CMS cms)
        {
            _adminInterface.AddSkill(cms);
            return RedirectToAction("MissionSkill", "Admin");
        }

        [HttpPost]
        public IActionResult addTheme(CMS cms)
        {
            _adminInterface.AddTheme(cms);
            return RedirectToAction("MissionTheme", "Admin");
        }

        [HttpPost]
        public IActionResult EditUserData(CMS cms)
        {
            _adminInterface.UpdateUserData(cms);
            return RedirectToAction("User", "Admin");
        }

        [HttpPost]
        public IActionResult EditSkillData(CMS cms)
        {
            _adminInterface.UpdateSkillData(cms);
            return RedirectToAction("MissionSkill", "Admin");
        }

        [HttpPost]
        public IActionResult EditThemeData(CMS cms)
        {
            _adminInterface.UpdateThemeData(cms);
            return RedirectToAction("MissionTheme", "Admin");
        }

        [HttpPost]
        public IActionResult approveStory(long storyId)
        {
            _adminInterface.approveStory(storyId);
            return Ok();
        }

        [HttpPost]
        public IActionResult rejectStory(long storyId)
        {
            _adminInterface.rejectStory(storyId);
            return Ok();
        }

        [HttpPost]
        public IActionResult deleteStory(long storyId)
        {
            _adminInterface.deleteStory(storyId);
            return Ok();
        }

        [HttpPost]
        public IActionResult approveApplication(long applicationId)
        {
            _adminInterface.approveApplication(applicationId);
            return Ok();
        }

        [HttpPost]
        public IActionResult rejectApplication(long applicationId)
        {
            _adminInterface.rejectApplication(applicationId);
            return Ok();
        }

        [HttpPost]
        public IActionResult deleteApplication(long applicationId)
        {
            _adminInterface.deleteApplication(applicationId);
            return Ok();
        }

        [HttpPost]
        public IActionResult deleteMission(long missionId)
        {
            _adminInterface.deleteMission(missionId);
            return Ok();
        }

        [HttpPost]
        public IActionResult deleteSkill(long skillId)
        {
            _adminInterface.deleteSkill(skillId);
            return Ok();
        }

        [HttpPost]
        public IActionResult deleteTheme(long themeId)
        {
            _adminInterface.deleteTheme(themeId);
            return Ok();
        }

        [HttpPost]
        public IActionResult deleteBanner(long bannerId)
        {
            _adminInterface.deleteBanner(bannerId);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteUserData(long userId)
        {
            _adminInterface.DeteleUserData(userId);
            return RedirectToAction("User", "Admin");
        }

        [HttpPost]
        public IActionResult DeleteCmsData(long cmsId)
        {
            _adminInterface.DeleteCmsPage(cmsId);
            return RedirectToAction("CMS", "Admin");
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

        public IActionResult MissionSkill()
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

        public IActionResult MissionApplication()
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

        public IActionResult MissionTheme()
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
