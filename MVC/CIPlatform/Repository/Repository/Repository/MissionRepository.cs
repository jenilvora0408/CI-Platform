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

namespace Repository.Repository.Repository
{
    public class MissionRepository: MissionInterface
    {
        private readonly CiPlatformContext _ciPlatformContext;
        public MissionRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
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

        

        //public List<MissionList> getMissionForGrid()
        //{
        //    var x = _ciPlatformContext.Missions.Where(x => x.CountryId == 1).Include(x => x.CountryId).FirstOrDefault();
        //    var y = x.Country.Name;

        //    var m = _ciPlatformContext.Missions.Where(x => x.ThemeId == 2).Include(x => x.ThemeId).FirstOrDefault();
        //    var n = m.

        //    List<MissionList> missionLists = new List<MissionList>();
        //    MissionList missionList = new MissionList();
        //    missionList.countryName = y;
        //    missionLists.Add(missionList);
        //    return missionLists;
        //}

        //public List<Mission> getMissionDatas()
        //{
        //    List<Mission> getMissionDatas = _ciPlatformContext.ExecuteStoreQuery<Mission>("exec GetEmployeeData").ToList();
        //}
    }
}
