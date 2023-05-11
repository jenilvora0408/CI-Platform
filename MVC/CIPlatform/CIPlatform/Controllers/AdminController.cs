using CIPlatform.Hubs;
using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Repository.Repository.Interface;

namespace CIPlatform.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AdminInterface _adminInterface;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public AdminController(ILogger<AccountController> logger, AdminInterface adminInterface, CiPlatformContext ciPlatformContext,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, MissionInterface missionInterface, IWebHostEnvironment env,
            IHubContext<NotificationHub> notificationHub)
        {
            _logger = logger;
            _adminInterface = adminInterface;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _notificationHub = notificationHub;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// View - CMS Page
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// View - Banner Page
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Shows list of CMS Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult CmsTable(string Search, int pageNumber)
        {
            var a = _adminInterface.GetCmsPages(Search, pageNumber);
            
            return PartialView("_CMSList", a);
        }

        /// <summary>
        /// Shows list of Banner Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult BannerTable(string Search, int pageNumber)
        {
            var banner = _adminInterface.GetBannerPages(Search, pageNumber);

            return PartialView("_BannerTable", banner);
        }

        /// <summary>
        /// Shows list of User Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult userTable(string Search, int pageNumber)
        {
            var b = _adminInterface.GetUserPages(Search, pageNumber);

            return PartialView("_UserTable", b);
        }

        /// <summary>
        /// Shows list of Story Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult storyTable(string Search, int pageNumber)
        {
            var c = _adminInterface.GetStoryPages(Search,pageNumber);

            return PartialView("_StoryTable", c);
        }

        /// <summary>
        /// Shows list of Mission Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult missionTable(string Search, int pageNumber)
        {
            var d = _adminInterface.GetMissionPages(Search,pageNumber);

            return PartialView("_MissionTable", d);
        }

        /// <summary>
        /// Shows list of Skill Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult skillTable(string Search, int pageNumber)
        {
            var e = _adminInterface.GetSkillPages(Search,pageNumber);

            return PartialView("_SkillTable", e);
        }

        /// <summary>
        /// Shows list of Mission Application Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult applicationTable(string Search, int pageNumber)
        {
            var f = _adminInterface.GetApplicationPages(Search,pageNumber);

            return PartialView("_ApplicationTable", f);
        }

        /// <summary>
        /// Shows list of Mission Theme Data
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IActionResult themeTable(string Search, int pageNumber)
        {
            var g = _adminInterface.GetThemePages(Search,pageNumber);

            return PartialView("_ThemeTable", g);
        }

        /// <summary>
        /// View - Add CMS Page
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// View - Add Mission Page
        /// </summary>
        /// <returns></returns>
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
            var missionskills = _adminInterface.GetSkills();
            ViewBag.missionskills = missionskills;
            var country = _adminInterface.GetCountries();
            ViewBag.Country = country;
            var theme = _adminInterface.GetThemes();
            ViewBag.theme = theme;

            return View(missionCrud);
        }

        
        /// <summary>
        /// Add mission, user can insert record in Mission, Mission Media, Mission Document, 
        /// Mission Skill & Mission Theme
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileImg"></param>
        /// <param name="fileDoc"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMission(MissionCrud model, IEnumerable<IFormFile> fileImg, IEnumerable<IFormFile> fileDoc)
        {
            if (ModelState.IsValid)
            {
                if (fileImg != null && fileImg.Count() > 0)
                {
                    var missionId = _adminInterface.AddMission(model);
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
                            ModelState.AddModelError("missionMedia", "Please select png, jpg or jpeg file to upload.");
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
                                ModelState.AddModelError("missionDocument", "Please select pdf, doc or xlsx file to upload.");
                            }
                        }
                    }
                    
                    return RedirectToAction("Mission");
                }
                else
                {
                    ModelState.AddModelError("missionMedia", "Please select at least 1 image");
                }
            }
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
            var missionskills = _adminInterface.GetSkills();
            ViewBag.missionskills = missionskills;
            var country = _adminInterface.GetCountries();
            ViewBag.Country = country;
            var theme = _adminInterface.GetThemes();
            ViewBag.theme = theme;

            return View(missionCrud);
        }

        /// <summary>
        /// Get Cities in Dropdown corresponding to selected Country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public JsonResult GetCitiesOfCountry(long country)
        {
            var cities = _adminInterface.GetCitiesOfCountry(country);

            return Json(cities);
        }

        /// <summary>
        /// View - Add Banner Page
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Admin can add Banner which would be displayed on Login Page 
        /// </summary>
        /// <param name="adminBannerModel"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// View - Add User Page
        /// </summary>
        /// <returns></returns>
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
            var country = _adminInterface.GetCountries();
            ViewBag.countries = country;

            return View(cms);
        }

        /// <summary>
        /// Autofill data in Edit User Page
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
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
            var country = _adminInterface.GetCountries();
            ViewBag.countrieS = country;

            return View(editUser);
        }

        /// <summary>
        /// Autofill data in Edit Mission Page
        /// </summary>
        /// <param name="MissionId"></param>
        /// <returns></returns>
        public IActionResult EditMission(long MissionId)
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var editMission = _adminInterface.GetMissionData(MissionId);
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            editMission.Navbar_1 = missionHomeModel;
            var selectedskills = _adminInterface.GetSelectedSkills(MissionId);
            ViewBag.selectedskills = selectedskills;
            var notselectedskills = _adminInterface.GetNotSelectedSkills(MissionId);
            ViewBag.notselectedskills = notselectedskills;
            var country = _adminInterface.GetCountries();
            ViewBag.Country = country;
            var theme = _adminInterface.GetThemes();
            ViewBag.theme = theme;

            return View(editMission);
        }

        /// <summary>
        /// Edit Missions 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileImg"></param>
        /// <param name="fileDoc"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditMission(MissionCrud model, IEnumerable<IFormFile> fileImg, IEnumerable<IFormFile> fileDoc)
        {
            if (ModelState.IsValid)
            {

                if (fileImg != null && fileImg.Count() > 0)
                {
                    var missionId = _adminInterface.EditMission(model);
                    _adminInterface.RemoveMissionMedia(missionId);
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
                            ModelState.AddModelError("missionMedia", "Please select png, jpg or jpeg file to upload.");
                        }
                    }
                    if (fileDoc != null && fileDoc.Count() > 0)
                    {
                        _adminInterface.RemoveMissionDocument(missionId);
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
                                ModelState.AddModelError("missionDocument", "Please select pdf, doc or xlsx file to upload.");
                            }
                        }
                    }

                    return RedirectToAction("Mission");
                }
                else
                {
                    ModelState.AddModelError("missionMedia", "Please select at least 1 image");
                }
            }
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

            var selectedskills = _adminInterface.GetSelectedSkills(model.MissionId);
            ViewBag.selectedskills = selectedskills;
            var notselectedskills = _adminInterface.GetNotSelectedSkills(model.MissionId);
            ViewBag.notselectedskills = notselectedskills;
            var country = _adminInterface.GetCountries();
            ViewBag.Country = country;
            var theme = _adminInterface.GetThemes();
            ViewBag.theme = theme;

            return View(missionCrud);
        }

        /// <summary>
        /// Autofill data in Edit Skill Page
        /// </summary>
        /// <param name="SkillId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Autofill data in Edit Theme Page
        /// </summary>
        /// <param name="missionThemeId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Autofill Data in Edit CMS Page
        /// </summary>
        /// <param name="cmsPageId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add CMS records to database
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="Slug"></param>
        /// <param name="Status"></param>
        /// <param name="demo"></param>
        /// <param name="cmsId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddCmsData(string Title, string Description, string Slug, string Status, string demo, long cmsId)
        {
            _adminInterface.AddCmsData(Title, Description, Slug, Status, demo, cmsId);

            return RedirectToAction("CMS", "Admin");
        }

        /// <summary>
        /// Add User records to database
        /// </summary>
        /// <param name="cms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddUserData(CMS cms)
        {
            var country = _adminInterface.GetCountries();
            ViewBag.CountrieS = country;
            _adminInterface.AddUserData(cms);


            return RedirectToAction("User", "Admin");
        }

        /// <summary>
        /// Add skill records to database
        /// </summary>
        /// <param name="cms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult addSkill(CMS cms)
        {
            var skills = _adminInterface.AddSkill(cms);
            if(skills == false)
            {
                ModelState.AddModelError("SkillName", "Skill already exists.");
                TempData["error"] = "Skill already exists.";
                return RedirectToAction("MissionSkill", "Admin", new{status = true});
            }
            else
            {
                return RedirectToAction("MissionSkill", "Admin", new { status = false });
            }
        }

        /// <summary>
        /// Add Mission Theme records to database
        /// </summary>
        /// <param name="cms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult addTheme(CMS cms)
        {
            var theme = _adminInterface.AddTheme(cms);
            if(theme == false)
            {
                ModelState.AddModelError("title", "Theme already exists.");
                TempData["error"] = "Theme already exists.";
                return RedirectToAction("MissionTheme", "Admin",  new {status = true});
            }
            else
            {
                return RedirectToAction("MissionTheme", "Admin", new {status = false});
            }
            
        }

        /// <summary>
        /// Edit records to User Table
        /// </summary>
        /// <param name="cms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditUserData(CMS cms)
        {
            var country = _adminInterface.GetCountries();
            ViewBag.CountrieS = country;
            _adminInterface.UpdateUserData(cms);

            return RedirectToAction("User", "Admin");
        }

        /// <summary>
        /// Edit records of Skill Table
        /// </summary>
        /// <param name="cms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditSkillData(CMS cms)
        {
            _adminInterface.UpdateSkillData(cms);

            return RedirectToAction("MissionSkill", "Admin");
        }

        /// <summary>
        /// Edit records of Mission Theme Table
        /// </summary>
        /// <param name="cms"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditThemeData(CMS cms)
        {
            _adminInterface.UpdateThemeData(cms);

            return RedirectToAction("MissionTheme", "Admin");
        }

        /// <summary>
        /// Approve story
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="approveUser"></param>
        /// <param name="approveStoryTitle"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> approveStory(long storyId, string approveUser, string approveStoryTitle)
        {
            string username = approveUser;
            _adminInterface.approveStory(storyId, approveStoryTitle);           
            await _notificationHub.Clients.User(username).SendAsync("ReceiveMsg", "Your story " + approveStoryTitle + " has been approved", storyId);
            return Ok();
        }

        /// <summary>
        /// Reject Story
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="rejectUser"></param>
        /// <param name="rejectStoryTitle"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> rejectStory(long storyId, string rejectUser, string rejectStoryTitle)
        {
         
            string username = rejectUser;
            _adminInterface.rejectStory(storyId, rejectStoryTitle);
            await _notificationHub.Clients.User(username).SendAsync("ReceiveMsg", "Your story " + rejectStoryTitle + " has been rejected", storyId);
            return Ok();
        }

        /// <summary>
        /// Delete Story
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="deleteUser"></param>
        /// <param name="deleteStoryTitle"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> deleteStory(long storyId, string deleteUser, string deleteStoryTitle)
        {
            string username = deleteUser;
            _adminInterface.deleteStory(storyId, deleteStoryTitle);
            await _notificationHub.Clients.User(username).SendAsync("ReceiveMsg", "Your story " + deleteStoryTitle + " has been deleted", storyId);
            return Ok();
        }

        /// <summary>
        /// Approve application of a User
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="approveName"></param>
        /// <param name="approveTitle"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> approveApplication(long applicationId, string approveName, string approveTitle, long approveMissionId)
        {
            _adminInterface.approveApplication(applicationId, approveTitle, approveMissionId);
            await _notificationHub.Clients.User(approveName).SendAsync("ReceiveMsg", "Your application for mission " + approveTitle + " has been approved", applicationId);
            return Ok();
        }

        /// <summary>
        /// Reject application of a user
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult rejectApplication(long applicationId, string rejectName, string rejectTitle, long rejectMissionId)
        {
            _adminInterface.rejectApplication(applicationId, rejectTitle, rejectMissionId);
             _notificationHub.Clients.User(rejectName).SendAsync("ReceiveMsg", "Your application for mission " + rejectTitle + " has been rejected", applicationId);
            return Ok();
        }

        /// <summary>
        /// Delete application of a user
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> deleteApplication(long applicationId, string deleteName, string deleteTitle, long deleteMissionId)
        {
            _adminInterface.deleteApplication(applicationId, deleteTitle, deleteMissionId);
            await _notificationHub.Clients.User(deleteName).SendAsync("ReceiveMsg", "Your application for mission " + deleteTitle + " has been deleted", applicationId);
            return Ok();
        }

        /// <summary>
        /// Delete Mission
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult deleteMission(long missionId)
        {
            _adminInterface.deleteMission(missionId);

            return Ok();
        }

        /// <summary>
        /// Delete Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult deleteSkill(long skillId)
        {
            _adminInterface.deleteSkill(skillId);

            return Ok();
        }

        /// <summary>
        /// Delete Theme
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult deleteTheme(long themeId)
        {
            _adminInterface.deleteTheme(themeId);

            return Ok();
        }

        /// <summary>
        /// Delete Banner
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult deleteBanner(long bannerId)
        {
            _adminInterface.deleteBanner(bannerId);

            return Ok();
        }

        /// <summary>
        /// Delete user record from User Table
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteUserData(long userId)
        {
            _adminInterface.DeteleUserData(userId);

            return RedirectToAction("User", "Admin");
        }

        /// <summary>
        /// Delete CMS record from CMS Table
        /// </summary>
        /// <param name="cmsId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteCmsData(long cmsId)
        {
            _adminInterface.DeleteCmsPage(cmsId);

            return RedirectToAction("CMS", "Admin");
        }

        /// <summary>
        /// View - User
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// View - Story
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// View - Mission
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// View - Mission Skill
        /// </summary>
        /// <returns></returns>
        public IActionResult MissionSkill(bool status)
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

        /// <summary>
        /// View - Mission Application
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// View - Mission Theme
        /// </summary>
        /// <returns></returns>
        public IActionResult MissionTheme(bool status)
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
