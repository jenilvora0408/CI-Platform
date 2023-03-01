using CI_PLATFORM_MAIN_ENTITIES.Models.ViewModels;
using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repository.Repository.Interface;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

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
        public IActionResult NewPassword(string? token)
        {

            var resetObj = _registerInterface.findUserByToken(token);
            TimeSpan remainingTime = DateTime.Now - resetObj.CreatedAt;

            int hour = remainingTime.Hours;

            if (hour >= 4)
            {
                _registerInterface.removeResetPasswordToekn(resetObj);
                return RedirectToAction("Login");
            }
            NewPassword newPasswordModel = new NewPassword();
            newPasswordModel.token = token;
            return View(newPasswordModel);
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

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword obj)
        {
            if (!_registerInterface.isEmailAvailable(obj.email))
            {
                ModelState.AddModelError("EmailId", "Email not found");
            }
            if (ModelState.IsValid)
            {

                string uuid = Guid.NewGuid().ToString();
                PasswordReset resetPasswordObj = new PasswordReset();
                resetPasswordObj.Email = obj.email;
                resetPasswordObj.Token = uuid;
                resetPasswordObj.CreatedAt = DateTime.Now;

                _registerInterface.addResetPasswordToken(resetPasswordObj);

                var userObj = _registerInterface.findUser(obj.email);
                int UserID = (int)userObj.UserId;
                string welcomeMessage = "Welcome to CI platform, <br/> You can Reset your password using below link. </br>";
                string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Account/NewPassword?token=" + uuid + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                MailHelper mailHelper = new MailHelper(configuration);
                ViewBag.sendMail = mailHelper.Send(obj.email, welcomeMessage + path);


                return View();
            }
            return View();
        }

        
        [HttpPost]
        public IActionResult NewPassword(NewPassword obj)
        {
            string token = obj.token;
            if (token != null)
            {
                if (obj.password.Equals(obj.confirm_password))
                {

                    var resetObj = _registerInterface.findUserByToken(token);
                    var userObj = _registerInterface.findUser(resetObj.Email);
                    if (!obj.password.Equals(userObj.Password))
                    {
                        userObj.Password = obj.password;
                        _registerInterface.updatePassword(userObj);
                        _registerInterface.removeResetPasswordToekn(resetObj);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("NewPassword", "You can not set Old password as New Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Confirm password does not match to new password");
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }
    }
}
