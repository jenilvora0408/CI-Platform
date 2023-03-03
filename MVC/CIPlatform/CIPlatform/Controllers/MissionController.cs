using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Repository.Interface;

namespace CIPlatform.Controllers
{
    public class MissionController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MissionInterface _missionInterface;

        public MissionController(ILogger<AccountController> logger, MissionInterface missionInterface)
        {
            _logger = logger;
            _missionInterface = missionInterface;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MissionListing()
        {   
            return View();
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

        public IActionResult MissionListing_List()
        {
            return View();
        }

        public IActionResult MissionVolunteering()
        {
            return View();
        }
    }
}
