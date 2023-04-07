using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Repository.Interface;

namespace CIPlatform.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ProfileInterface _profileInterface;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ProfileController(ILogger<AccountController> logger, ProfileInterface profileInterface, CiPlatformContext ciPlatformContext,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, MissionInterface missionInterface, IWebHostEnvironment env)
        {
            _logger = logger;
            _profileInterface = profileInterface;
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

        public IActionResult EditProfile()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            editProfile edit = new editProfile();
            Navbar_1 missionHomeModel = new Navbar_1();
            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
            missionHomeModel.userId = userObj.UserId;
            edit.Navbar_1 = missionHomeModel;

            var country = _ciPlatformContext.Countries.Select(x => new SelectListItem { Value = x.CountryId.ToString(), Text = x.Name }).ToList();
            ViewBag.Country = country;
            var email = HttpContext.Session.GetString("useremail");
            User user = _missionInterface.findUser(userSessionEmailId);
            var selectedskills = _ciPlatformContext.UserSkills.Where(us => us.UserId == user.UserId).Join(_ciPlatformContext.Skills, us => us.SkillId, s => s.SkillId, (us, s) => new SelectListItem { Value = s.SkillId.ToString(), Text = s.SkillName }).ToList();
            ViewBag.selectedskills = selectedskills;
            var notselectedskills = _ciPlatformContext.Skills.Where(s => !_ciPlatformContext.UserSkills.Where(us => us.UserId == user.UserId).Select(us => us.SkillId).Contains(s.SkillId)).Select(s => new SelectListItem { Value = s.SkillId.ToString(), Text = s.SkillName }).ToList();

            ViewBag.notselectedskills = notselectedskills;
            var model = _profileInterface.PutUserDetails(edit, email);

            return View(edit);
        }

        [HttpPost]
        public IActionResult changePasswordForProfile(editProfile edit)
        {
           
                string userSessionEmailId = HttpContext.Session.GetString("useremail");
                var a = _profileInterface.changePassword(edit, userSessionEmailId);
                if (!a)
                {
                    ModelState.AddModelError("oldPassword", "Incorrect old password.");
                }
            
            
            return RedirectToAction("EditProfile");
        }

        
        [HttpPost]
        public IActionResult SaveUserEditProfile(editProfile N)
        {
            var email = HttpContext.Session.GetString("useremail");
            var model = _profileInterface.SaveUserDetail(N, email);
            return RedirectToAction("EditProfile");
        }


        [HttpPost]
        public IActionResult UserContactUs(editProfile N)
        {
            var email = HttpContext.Session.GetString("useremail");
            User user = _missionInterface.findUser(email);
            string welcomeMessage = "Welcome to CI platform, <br/> " + user.FirstName + " " + user.LastName + "(" + user.Email + ") Want to contact to you.";
            string path = "<br/>" + N.Message + "";
            MailManager mailHelper = new MailManager(_configuration);
            string subject = N.Subject;
            ViewBag.sendMail = mailHelper.Send(user.Email, subject, welcomeMessage + path);
            ModelState.AddModelError("Email", "Email sent successfully.");
            return RedirectToAction("EditProfile");

        }

        [HttpPost]
        public IActionResult UpdateUserImage(IFormFile inputFiles)
        {
            var email = HttpContext.Session.GetString("useremail");
            User user = _missionInterface.findUser(email);
            if (inputFiles != null)
            {
                string fileName = Path.GetFileName(inputFiles.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "userAvatar", fileName);
                var imgpath = "/userAvatar/" + fileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    inputFiles.CopyTo(stream);
                }
                var userImgPath = _profileInterface.ChangeUserProfileImage(imgpath, user.UserId);

                HttpContext.Session.SetString("Avatar", imgpath);
                //TempData["Success"] = "User Image Upload Successfully";
                return Content("success");
            }
            else
            {
                return Content("error");
            }

        }
    }
}
