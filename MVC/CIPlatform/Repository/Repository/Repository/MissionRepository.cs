using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Repository.Repository.Repository
{
    public class MissionRepository : MissionInterface
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        private readonly CiPlatformContext _ciPlatformContext;
        public MissionRepository(CiPlatformContext ciPlatformContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public User findUser(string email)
        {
            return _ciPlatformContext.Users.Where(u => u.Email == email).First();
        }
        public List<Country> GetCountries()
        {
            List<Country> countries = _ciPlatformContext.Countries.ToList();
            return countries;
        }
        public List<City> GetCities()
        {
            List<City> cities = _ciPlatformContext.Cities.ToList();
            return cities;
        }
        public List<MissionTheme> GetMissionThemes()
        {
            List<MissionTheme> missionThemes = _ciPlatformContext.MissionThemes.ToList();
            return missionThemes;
        }
        public List<Skill> GetSkills()
        {
            List<Skill> skills = _ciPlatformContext.Skills.ToList();
            return skills;
        }
        public List<Mission> GetMissions()
        {
            List<Mission> missions = _ciPlatformContext.Missions.ToList();
            return missions;
        }
        public List<GoalMission> GetGoalMissions()
        {
            List<GoalMission> goalMissions = _ciPlatformContext.GoalMissions.ToList();
            return goalMissions;
        }
        public void addFavouriteMission(FavouriteMission favouriteMissionObj)
        {
            _ciPlatformContext.FavouriteMissions.Add(favouriteMissionObj);
            _ciPlatformContext.SaveChanges();
        }

        public void removeFavouriteMission(FavouriteMission favouriteMissionObj)
        {
            _ciPlatformContext.FavouriteMissions.Remove(favouriteMissionObj);
            _ciPlatformContext.SaveChanges();
        }
        public FavouriteMission getFavouriteMission(FavouriteMission favouriteMissionObj)
        {
            FavouriteMission favouriteMission = _ciPlatformContext.FavouriteMissions.Where(u => u.UserId == favouriteMissionObj.UserId && u.MissionId == favouriteMissionObj.MissionId).First();

            return favouriteMission;
        }
        public IEnumerable<FavouriteMission> getFavouriteMissionsOfUser(int userid)
        {
            return _ciPlatformContext.FavouriteMissions.Where(u => u.UserId == userid);
        }
        public long GetUserID(string Email)
        {
            User user = _ciPlatformContext.Users.Where(x => x.Email == Email).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }
            else
            {

                return user.UserId;
            }
        }

        public Mission GetMissionByMissionId(int MissionId)
        {
            return _ciPlatformContext.Missions.Where(u => u.MissionId.Equals(MissionId)).First();
        }
        public Boolean RecommandtoCoWorker(long fromUserId, int MissionId, long toUserId)
        {
            var shareMission = _ciPlatformContext.MissionInvites.FirstOrDefault(x => x.FromUserId == fromUserId && x.ToUserId == toUserId && x.MissionId == MissionId);

            if (shareMission != null)
            {
                return true;
            }
            else
            {
                MissionInvite missioninvite = new MissionInvite();
                missioninvite.FromUserId = fromUserId;
                missioninvite.MissionId = MissionId;
                missioninvite.ToUserId = toUserId;
                missioninvite.CreatedAt = DateTime.Now;
                _ciPlatformContext.MissionInvites.Add(missioninvite);
                _ciPlatformContext.SaveChanges();
                return false;
            }

        }

        public void addComments(long missionid, long userid, string commented)
        {
            Comment comment = new Comment();
            comment.MissionId = missionid;
            comment.UserId = userid;
            comment.CreatedAt = DateTime.Now;
            comment.CommentText = commented;
            _ciPlatformContext.Comments.Add(comment);
            _ciPlatformContext.SaveChangesAsync();
        }

        MissionVol MissionInterface.getMissionVolData(int? missionId, int? userId)
        {
            MissionVol missionVol = new MissionVol();
            missionVol.missionDocument = _ciPlatformContext.MissionDocuments.Where(x => x.MissionId == missionId).AsEnumerable().ToList();
            missionVol.Volunteering = _ciPlatformContext.Volunteerings.FromSqlInterpolated($"exec GetMissionVolData @missionId={missionId}, @userId={userId}").AsEnumerable().First();
            return missionVol;
        }

        List<RecentVolunteer> MissionInterface.getRelatedMissions(int? missionId)
        {
            RecentVolunteer recentVolunteer = new RecentVolunteer();
            return _ciPlatformContext.RecentVolunteer.FromSqlInterpolated($"exec recentVolunteer @missionid={missionId} ").ToList();
        }

        public IEnumerable<RelatedMission> GetRelatedMissions(string theme, int? missionID)
        {
            return _ciPlatformContext.RelatedMissions.FromSqlInterpolated($"exec RelatedMissionData @themeTitle={theme}, @missionID={missionID}");
        }

        public Pagination GetMissionsByUtilities(Utilities utilities, int userId)
        {
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output_1 = new SqlParameter("@MissionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };

            var missions = _ciPlatformContext.MissionList.FromSqlInterpolated(
                $"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}, @TotalCount = {output} out, @MissionCount = {output_1} out,@userId={userId}")
                .ToList();

            var pagination = new Pagination();
            pagination.missions = missions;
            pagination.pageSize = 9;
            pagination.pageCount = long.Parse(output.Value.ToString());
            pagination.totalMissionCount = long.Parse(output_1.Value.ToString());
            pagination.activePage = utilities.pageNumber;

            return pagination;
        }

        public Pagination GetMissionByUtilitiesForList(Utilities utilities, int userId)
        {
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            var output_1 = new SqlParameter("@MissionCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            
            List<MissionList> test = _ciPlatformContext.MissionList.FromSqlInterpolated(
                $"exec GetMissionData @searchCountry = {utilities.country}, @searchCity = {utilities.city}, @searchTheme = {utilities.theme}, @searchSkill = {utilities.skill}, @search = {utilities.search}, @sortBy = {utilities.sortBy}, @pageNumber = {utilities.pageNumber}, @TotalCount = {output} out, @MissionCount = {output_1} out, @userId={userId}")
                .ToList();

            var pagination = new Pagination();
            pagination.missions = test;
            pagination.pageSize = 9;
            pagination.pageCount = long.Parse(output.Value.ToString());
            pagination.totalMissionCount = long.Parse(output_1.Value.ToString());
            pagination.activePage = utilities.pageNumber;

            return pagination;
        }

        public List<Comment> GetCommentsByMissionId(int missionId)
        {
            return _ciPlatformContext.Comments.Where(x => x.MissionId == missionId).Include(x => x.User).AsEnumerable().ToList();
        }

    }
}
