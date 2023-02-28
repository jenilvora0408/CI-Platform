using CI_PLATFORM_MAIN_ENTITIES.Models.ViewModels;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
      
        public AccountController(ILogger<AccountController> logger, RegisterInterface registerInterface, CiPlatformContext ciPlatformContext,IHttpContextAccessor httpContextAccessor,
                              IConfiguration _configuration)
        {
            
            _logger = logger;
           _registerInterface = registerInterface;
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
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
        public IActionResult NewPassword()
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
        
        //[HttpPost]
        //public IActionResult ValidateForgotPassword(ForgotPassword N)
        //{
        //    return RedirectToAction("Login");
        //}

        public IActionResult ValidateNewPassword(NewPassword N)
        {
            return RedirectToAction("Login");
     
        }
        [HttpPost]
        public IActionResult ValidateForgotDetails(ForgotPassword fpm)
        {
            if (_registerInterface.isEmailAvailable(fpm.email))
            {
                try
                {
                    long UserID = _registerInterface.GetUserID(fpm.email);
                    string welcomeMessage = "Welcome to CI platform, <br/> You can Reset your password using below link. </br>";
                    string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Account/NewPassword/" + UserID.ToString() + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                    MailHelper mailHelper = new MailHelper(configuration);
                    ViewBag.sendMail = mailHelper.Send(fpm.email, welcomeMessage + path);
                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "error1");
                ViewBag.isForgetPasswordOpen = true;
            }



            return View("Login");
        }

        public IActionResult Reset(int id)
        {
            return View();
        }
        public IActionResult Reset(NewPassword rpm, int id)
        {
            if (ModelState.IsValid)
            {

                if (_registerInterface.ChangePassword(id, rpm))
                {
                    ModelState.Clear();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "error2");
                }


            }

            return View();
        }
    }

}
