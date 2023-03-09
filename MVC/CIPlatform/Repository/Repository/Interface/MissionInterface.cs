using Entities.Models;
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
    }
}
