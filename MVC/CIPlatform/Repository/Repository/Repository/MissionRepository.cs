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

namespace Repository.Repository.Repository
{
    public class MissionRepository: MissionInterface
    {
        private readonly CiPlatformContext _ciPlatformContext;
        public MissionRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
        }
        public User findUser(string email)
        {
            return _ciPlatformContext.Users.Where(u => u.Email.Equals(email)).First();
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
        public void addToFavourites(long missionid, long userid, int fav)
        {
            if (fav == 1)
            {
                FavouriteMission favouriteMission = new FavouriteMission();
                favouriteMission.MissionId = missionid;
                favouriteMission.UserId = userid;
                favouriteMission.CreatedAt = DateTime.Now;
                _ciPlatformContext.FavouriteMissions.Add(favouriteMission);
                _ciPlatformContext.SaveChangesAsync();

            }
            else
            {
                FavouriteMission favouriteMission = _ciPlatformContext.FavouriteMissions.Where(x => x.MissionId == missionid && x.UserId == userid).FirstOrDefault();
                _ciPlatformContext.FavouriteMissions.Remove(favouriteMission);
                _ciPlatformContext.SaveChangesAsync();
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
    }

}
