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
        User MissionInterface.findUser(string email)
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

    }
}
