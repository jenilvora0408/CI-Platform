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
    public class MissionController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MissionInterface _missionInterface;
        private readonly CiPlatformContext _ciPlatformContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;


        public MissionController(ILogger<AccountController> logger, MissionInterface missionInterface, CiPlatformContext ciPlatformContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _missionInterface = missionInterface;
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
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

            public IActionResult MissionVolunteering(int? missionId)
            {
                string userSessionEmailId = HttpContext.Session.GetString("useremail");
                User userObj = _missionInterface.findUser(userSessionEmailId);
            if (userSessionEmailId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            int userId = (int)userObj.UserId;
            MissionVol missionVol =_missionInterface.getMissionVolData(missionId,userId);
            //missionVol.missionDocument = _ciPlatformContext.MissionDocuments.Where(x => x.MissionId == missionId).AsEnumerable().ToList();
            //missionVol.Volunteering = _ciPlatformContext.Volunteerings.FromSqlInterpolated($"exec GetMissionVolData @missionId={missionId}, @userId={userObj.UserId}").AsEnumerable().First();
            Navbar_1 navbar1 = new Navbar_1();
            missionVol.Navbar_1 = new Navbar_1();
            missionVol.Navbar_1.username = userObj.FirstName + " " + userObj.LastName;
            missionVol.Navbar_1.avatar = userObj.Avatar;
            missionVol.Navbar_1.userId = userObj.UserId;
            missionVol.recentVolunteer = _missionInterface.getRelatedMissions(missionId);

            return View(missionVol);
            }

            //public IActionResult RecentVolunteers(int? missionId)
            //{
            //    MissionVol missionVol = new MissionVol();
            //    missionVol.recentVolunteer = _ciPlatformContext.MissionVols.FromSqlInterpolated($"exec recentVolunteer").ToList();
            //    return View(missionVol);
            //}


            [HttpPost]
            public IActionResult relatedMissions(string theme, int? missionID)
            {
                IEnumerable<RelatedMission> relatedMission=_ciPlatformContext.RelatedMissions.FromSqlInterpolated($"exec RelatedMissionData @themeTitle={theme}, @missionID={missionID}");
                return PartialView("RelatedMission",relatedMission);
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

            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            // make explicit SQL Parameter
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output_1 = new SqlParameter("@MissionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            Pagination pagination = new Pagination();
            List<MissionList> test = _ciPlatformContext.MissionList.FromSqlInterpolated($"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}, @TotalCount = {output} out, @MissionCount = {output_1} out,@userId={userObj.UserId}").ToList();
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
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSessionEmailId);
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output_1 = new SqlParameter("@MissionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            Pagination pagination = new Pagination();
            List<MissionList> test = _ciPlatformContext.MissionList.FromSqlInterpolated($"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}, @TotalCount = {output} out, @MissionCount = {output_1} out, @userId={userObj.UserId}").ToList();
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


        [HttpPost]
        public IActionResult RecommandtoCoWorker(string InviteEmail, long MissionId)
        {
            
            string email = HttpContext.Session.GetString("useremail");
            var userfrom = _missionInterface.findUser((string)email);
            var userto = _missionInterface.findUser((string)InviteEmail);
            
            bool shareMessage = _missionInterface.RecommandtoCoWorker((long)userfrom.UserId, (int)MissionId, (long)userto.UserId);
            string message = "";
            if (shareMessage)
            {
                message = "You Alerady Invited";
                ViewBag.Message = "You Alerady Invited";
            }
            else
            {
                string mailmessage = "Welcome to CI Platform, <br/> your friend " + userfrom.FirstName + " " + userfrom.LastName + " " + "You can Join Mission Using Below link.. </br>";
                string path = "<a href=\"" + "https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Mission/MissionVolunteering?MissionId=" + MissionId + "\"style=\"font-weight:500;color:blue;\">Invite to Mission</a>";
                //string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Mission/MissionVolunteering?token=" + MissionId + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                MailHelper mailHelper = new MailHelper(_configuration);
                ViewBag.sendMail = mailHelper.Send(InviteEmail, mailmessage + path);
                message = "You Successfully Invited";
                ViewBag.Message = "You Successfully Invited";
                ModelState.Clear();
            }
            return Json(new { data = message });
        }


        public IActionResult MissionUserRating(float ratingCount, long? missionid)
        {
            //var missionid = HttpContext.Session.GetInt32("missionid");
            var Email = HttpContext.Session.GetString("useremail");
            User user = _missionInterface.findUser((string)Email);
            //MissionRating missionRating = new MissionRating();
            var findUserRating = _ciPlatformContext.MissionRatings.Where(x => x.UserId == user.UserId && x.MissionId == missionid).FirstOrDefault();
            if (findUserRating == null)
            {
                if (ratingCount == 0.5)
                {
                    ratingCount = 1;
                }
                MissionRating missionRating = new MissionRating();
                missionRating.UserId = user.UserId;
                missionRating.MissionId = (long)missionid;
                missionRating.Rating = (int)ratingCount;
                var entry = _ciPlatformContext.MissionRatings.Add(missionRating);
                _ciPlatformContext.SaveChanges();
            }
            else
            {
                if (ratingCount == 0.5)
                {
                    ratingCount = 1;
                }
                findUserRating.UserId = user.UserId;
                findUserRating.MissionId = (long)missionid;
                findUserRating.Rating = (int)ratingCount;
                findUserRating.UpdatedAt = DateTime.Now;
                _ciPlatformContext.MissionRatings.Update(findUserRating);
                _ciPlatformContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ApplyNow(long? missionID, long? userID)
        {
            //var Email = HttpContext.Session.GetString("useremail");
            //User user = _missionInterface.findUser((string)Email);
            var applyNow = _ciPlatformContext.MissionApplications.Where(x => x.UserId == userID && x.MissionId == missionID).FirstOrDefault();
            if (applyNow == null)
            {
                MissionApplication missionApplication = new MissionApplication();
                missionApplication.UserId = userID;
                missionApplication.MissionId = missionID;
                missionApplication.ApprovalStatus = "Pending";
                missionApplication.AppliedAt = DateTime.Now;
                var missionApply = _ciPlatformContext.MissionApplications.Add(missionApplication);
                _ciPlatformContext.SaveChanges();
            }
            else
            {
                applyNow.UserId = userID;
                applyNow.MissionId = missionID;
                applyNow.ApprovalStatus = "Deleted";
                applyNow.AppliedAt = DateTime.Now;
                _ciPlatformContext.MissionApplications.Remove(applyNow);
                _ciPlatformContext.SaveChanges();
            }
            return Ok();
        }


        [HttpPost]
        public void PostComments(String MissionID, String comment)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSession);
            long misid = Int64.Parse(MissionID);
            int missid = Int32.Parse(MissionID);
            _missionInterface.addComments(misid, userObj.UserId, comment);
            //List<Comment> comment1 = _context.Comments.Where(x => x.MissionId == missid).AsEnumerable().ToList();

        }
        public IActionResult ListComments(String MissionID)
        {
            string userSession = HttpContext.Session.GetString("useremail");
            User userObj = _missionInterface.findUser(userSession);
            long misid = Int64.Parse(MissionID);
            int missid = Int32.Parse(MissionID);
            //_missionListingRepository.addComments(misid, userObj.UserId, comment);
            List<Comment> comment1 = _ciPlatformContext.Comments.Where(x => x.MissionId == missid).Include(x => x.User).AsEnumerable().ToList();
            return PartialView("_Comments", comment1);
        }


    }
}
