﻿using Entities.Data;
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
            return _ciPlatformContext.Users.Where(u => u.Email == email).Include(x => x.Country).Include(x => x.City).First();
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
            List<MissionTheme> missionThemes = _ciPlatformContext.MissionThemes.Where(x => x.DeletedAt == null && x.Status == true).ToList();
            return missionThemes;
        }
        public List<Skill> GetSkills()
        {
            List<Skill> skills = _ciPlatformContext.Skills.Where(x => x.DeletedAt == null && x.Status == true).ToList();
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

        public Mission FindMissionTitle(long MissionId)
        {
            return _ciPlatformContext.Missions.Where(x => x.MissionId == MissionId).First();
        }
        
        public void addNotificationForRecommendMission(long MissionId, long userId, string title, string usernameFrom)
        {
            NotificationSetting notificationSetting = _ciPlatformContext.NotificationSettings.Where(x => x.UserId == userId && x.NotificationType == "Recommend Mission" && x.Status == true).FirstOrDefault();
            if (notificationSetting != null)
            {
                Notification notification = new Notification();
                notification.NotificationMessage = "Your friend " + usernameFrom + " has invited you to join mission - " + title;
                notification.UserId = userId;
                notification.MissionId = MissionId;
                notification.Status = false;
                notification.NotificationType = "Mission";
                _ciPlatformContext.Add(notification);
                _ciPlatformContext.SaveChanges();
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
            _ciPlatformContext.SaveChanges();
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

        public IEnumerable<RelatedMission> GetRelatedMissions(string theme, int? missionID, int? userId)
        {
            var mission = _ciPlatformContext.Missions.Where(x => x.MissionId == missionID).First();
            var relatedMission = _ciPlatformContext.RelatedMissions.FromSqlInterpolated($"exec RelatedMissionData @themeTitle={theme}, @missionID={missionID}, @cityid={mission.CityId}, @countryid={mission.CountryId}, @userId={userId}");
            return relatedMission;
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


        public List<City> GetCitiesByCountryId(int countryId)
        {
            return _ciPlatformContext.Cities.Where(c => c.CountryId == countryId).ToList();
        }

        List<Notification> MissionInterface.GetNotifications(int userId)
        {
            return _ciPlatformContext.Notifications.Where(x => x.UserId == userId && x.DeletedAt == null).ToList();
        }

        public void ClearNotifications(User user)
        {
            var notification = _ciPlatformContext.Notifications.Where(x => x.UserId == user.UserId);
            foreach(var item in notification)
            {
                item.DeletedAt = DateTime.Now;
            }
            _ciPlatformContext.SaveChanges();
        }

        public void NotifyStatus(long notificationId)
        {
            var notification = _ciPlatformContext.Notifications.Where(x => x.NotificationId == notificationId).FirstOrDefault();
            if(notification.Status == true)
            {
                notification.Status = false;
            }
            else
            {
                notification.Status=true;
            }
            _ciPlatformContext.SaveChanges();
        }

        public UserNotificationSetting GetListOfNotificationSetting(long userId)
        {
            NotificationSetting setting = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Mission Approved").First();
            NotificationSetting setting1 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "NewMission").First();
            NotificationSetting setting2 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Recommend Story").First();
            NotificationSetting setting3 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Recommend Mission").First();
            NotificationSetting setting4 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Story Published").First();
            NotificationSetting setting5 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Email").First();
            UserNotificationSetting userNotification = new UserNotificationSetting();
            userNotification.missionApplication = setting.Status;
            userNotification.newMission = setting1.Status ;
            userNotification.recommendstory = setting2.Status;
            userNotification.recommendMission = setting3.Status;
            userNotification.myStory = setting4.Status;
            userNotification.email = setting5.Status;
            return userNotification;
        }

        public void UpdateNotificationSetting(UserNotificationSetting userNotification, long userId)
        {
            NotificationSetting setting = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Mission Approved").First();
            NotificationSetting setting1 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "NewMission").First();
            NotificationSetting setting2 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Recommend Story").First();
            NotificationSetting setting3 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Recommend Mission").First();
            NotificationSetting setting4 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Story Published").First();
            NotificationSetting setting5 = _ciPlatformContext.NotificationSettings.Where(X => X.UserId == userId && X.NotificationType == "Email").First();
            setting.Status = userNotification.missionApplication;
            setting1.Status = userNotification.newMission;
            setting2.Status = userNotification.recommendstory;
            setting3.Status = userNotification.recommendMission;
            setting4.Status = userNotification.myStory;
            setting5.Status = userNotification.email;
            _ciPlatformContext.SaveChanges();

        }
    }
}
