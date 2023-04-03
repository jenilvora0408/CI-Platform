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

        public ProfileController(ILogger<AccountController> logger, ProfileInterface profileInterface, CiPlatformContext ciPlatformContext,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, MissionInterface missionInterface)
        {
            _logger = logger;
            _profileInterface = profileInterface;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
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
            User user = _ciPlatformContext.Users.Where(x => x.Email == email).FirstOrDefault();
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

    }
}
