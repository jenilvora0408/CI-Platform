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
            missionHomeModel.userId = userObj.UserId;
          
            return View(missionHomeModel);
        }

        public IActionResult MissionVolunteering()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Navbar_1 navbar1 = new Navbar_1();
            User userObj = _missionInterface.findUser(userSessionEmailId);

            navbar1.username = userObj.FirstName + " " + userObj.LastName;
            navbar1.avatar = userObj.Avatar;
            navbar1.userId = userObj.UserId;
            return View(navbar1);
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



        [HttpPost]
        public IActionResult gridSP(Utilities utilities)
        {
            // make explicit SQL Parameter
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output_1 = new SqlParameter("@MissionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            Pagination pagination = new Pagination();
            List<MissionList> test = _ciPlatformContext.MissionList.FromSqlInterpolated($"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}, @TotalCount = {output} out, @MissionCount = {output_1} out").ToList();
            pagination.missions = test;
            pagination.pageSize = 9;
            pagination.pageCount = long.Parse(output.Value.ToString());
            pagination.totalMissionCount = long.Parse(output_1.Value.ToString());
            pagination.activePage = utilities.pageNumber;
            return PartialView("_Grid", pagination);
        }

        [HttpPost]
        public IActionResult listSP(Utilities utilities)
        {
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output_1 = new SqlParameter("@MissionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            Pagination pagination = new Pagination();
            List<MissionList> test = _ciPlatformContext.MissionList.FromSqlInterpolated($"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}, @TotalCount = {output} out, @MissionCount = {output_1} out").ToList();
            pagination.missions = test;
            pagination.pageSize = 9;
            pagination.pageCount = long.Parse(output.Value.ToString());
            pagination.totalMissionCount = long.Parse(output_1.Value.ToString());
            pagination.activePage = utilities.pageNumber;
            return PartialView("_List", pagination);
        }


        public void addFavouriteMissions(string userId, string missionId)
        {
            FavouriteMission favouriteMissionObj = new FavouriteMission();
            favouriteMissionObj.UserId = Int64.Parse(userId);
            favouriteMissionObj.MissionId = Int64.Parse(missionId);
            _missionInterface.addFavouriteMission(favouriteMissionObj);
        }
        public void removeFavouriteMissions(string userId, string missionId)
        {
            FavouriteMission favouriteMissionObj = new FavouriteMission();
            favouriteMissionObj.UserId = Int64.Parse(userId);
            favouriteMissionObj.MissionId = Int64.Parse(missionId);
            FavouriteMission favouriteMission = _missionInterface.getFavouriteMission(favouriteMissionObj);
            _missionInterface.removeFavouriteMission(favouriteMission);
        }
        public IActionResult getFavouriteMissionsOfUser(int userid)
        {
            IEnumerable<FavouriteMission> favouriteMissions = _missionInterface.getFavouriteMissionsOfUser(userid);
            string arr = "";
            foreach (FavouriteMission favouriteMission in favouriteMissions)
            {
                arr += favouriteMission.MissionId + ",";
            }
            return Json(new { data = arr });
        }
    }
}
