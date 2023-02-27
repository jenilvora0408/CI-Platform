using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.Interface;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly RegisterInterface _registerInterface;
        private readonly CiPlatformContext _ciPlatformContext;
        public AccountController(ILogger<AccountController> logger, RegisterInterface registerInterface, CiPlatformContext ciPlatformContext)
        {
            _logger = logger;
           _registerInterface = registerInterface;
            _ciPlatformContext = ciPlatformContext;
        }
        public IActionResult Login()
        {
            return View();
        }
       
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register user)
        {
            var emailExist = _registerInterface.isEmailAvailable(user.email);


            if (emailExist)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View("Register");
            }
            User u = new User();
            u.FirstName = user.first_name;
            u.LastName = user.last_name;
            u.Email = user.email;
            u.PhoneNumber = user.phone_number;
            u.Password = user.password;
            _registerInterface.InsertUser(u);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult NewPassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ValidateLogin(Login N)
        {
            if (ModelState.IsValid)
            {
                var emailExist = _registerInterface.isEmailAvailable(N.email);
                if (emailExist)
                {
                    var password = _registerInterface.isPasswordAvailable(N.password, N.email);
                    if(password != null)
                    {
                        return RedirectToAction("MissionListing", "Mission");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Enter correct password");
                    }
                    return View("Login");
                }
                else
                {
                    ModelState.AddModelError("email", "Enter correct email address");
                    return View("Login");
                }
            }
            return RedirectToAction("Login");
        }
        
        public IActionResult ValidateForgotPassword(ForgotPassword N)
        {
            return RedirectToAction("Login");
        }

        public IActionResult ValidateRegister(Register N)
        {
            return RedirectToAction("Login");
        }

        public IActionResult ValidateNewPassword(NewPassword N)
        {
            return RedirectToAction("Login");
        }
    }
}
