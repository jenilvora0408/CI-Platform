using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface MissionInterface
    {
        //public MissionRepository(CiPlatformContext ciPlatformContext);
        public List<Country> GetCountries();
        public List<City> GetCities();
        public List<MissionTheme> GetMissionThemes();
        public List<Skill> GetSkills();
        public List<Mission> GetMissions();
        public List<GoalMission> GetGoalMissions();
        public User findUser(string email);
        public void addFavouriteMission(FavouriteMission favouriteMissionObj);
        public void removeFavouriteMission(FavouriteMission favouriteMissionObj);
        public FavouriteMission getFavouriteMission(FavouriteMission favouriteMissionObj);
        public IEnumerable<FavouriteMission> getFavouriteMissionsOfUser(int userid);
        public long GetUserID(string Email);
        public Mission GetMissionByMissionId(int MissionId);
        public Boolean RecommandtoCoWorker(long fromUserId, int MissionId, long toUserId);
        public void addComments(long missionid, long userid, string commented);
        public MissionVol getMissionVolData(int? missionId,int? userId);
        public List<RecentVolunteer> getRelatedMissions(int? missionId);
        public IEnumerable<RelatedMission> GetRelatedMissions(string theme, int? missionID, int? userId);
        public Pagination GetMissionsByUtilities(Utilities utilities, int userId);
        public Pagination GetMissionByUtilitiesForList(Utilities utilities, int userId);
        public List<Comment> GetCommentsByMissionId(int missionId);
        public List<City> GetCitiesByCountryId(int countryId);
    }
}
