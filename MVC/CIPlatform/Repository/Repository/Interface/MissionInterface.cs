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
        public List<Country> GetCountries();
        public List<City> GetCities();
        public List<MissionTheme> GetMissionThemes();
        public List<Skill> GetSkills();
    }
}
