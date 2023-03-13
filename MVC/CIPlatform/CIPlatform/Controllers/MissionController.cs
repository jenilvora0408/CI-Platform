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
using System.Data;
using Dapper;

namespace CIPlatform.Controllers
{
    public class MissionController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;

        public MissionController(ILogger<AccountController> logger, MissionInterface missionInterface, CiPlatformContext ciPlatformContext)
        {
            _logger = logger;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MissionListing()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Navbar_1 missionHomeModel = new Navbar_1();
            User userObj = _missionInterface.findUser(userSessionEmailId);

            missionHomeModel.username = userObj.FirstName + " " + userObj.LastName;
            missionHomeModel.avatar = userObj.Avatar;
          
            return View(missionHomeModel);
        }

        public IActionResult listCountries()
        {
            IEnumerable<Country> countries = _missionInterface.GetCountries();
            return Json(countries);
        }
        public IActionResult listCities()
        {
            IEnumerable<City> cities = _missionInterface.GetCities();
            return Json(cities);
        }
        public IActionResult listTheme()
        {
            IEnumerable<MissionTheme> missionThemes = _missionInterface.GetMissionThemes(); 
            return Json(missionThemes);
        }
        public IActionResult listSkill()
        {
            IEnumerable<Skill> missionSkills = _missionInterface.GetSkills();
            return Json(missionSkills);
        }
        public IActionResult listMission()
        {
            IEnumerable<Mission> missions = _missionInterface.GetMissions();
            return Json(missions);
        }
        public IActionResult listGoalMission()
        {
            IEnumerable<GoalMission> goalMissions = _missionInterface.GetGoalMissions();
            return Json(goalMissions);
        }

        public IActionResult MissionListing_List()
        {
            return View();
        }

        public IActionResult MissionVolunteering()
        {
            return View();
        }
        [HttpPost]
        public IActionResult gridSP(Utilities utilities)
        {
            List<MissionList> test = _ciPlatformContext.MissionList.FromSqlInterpolated($"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}").ToList();
            return PartialView("_Grid", test);
        }
        public IActionResult listSP()
        {
            List<MissionList> listing = _ciPlatformContext.MissionList.FromSqlInterpolated($"exec GetMissionData").ToList();
            return PartialView("_List", listing);
        }
        

    }
}
