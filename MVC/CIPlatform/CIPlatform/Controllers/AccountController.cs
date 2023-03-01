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
        public IActionResult NewPassword(string? email)
        {
            NewPassword newPassword = new NewPassword();
            newPassword.email = email;
            return View(newPassword);
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
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var emailExist = _registerInterface.isEmailAvailable(model.email);
                if (emailExist)
                {
                    var lnkHref = "<a href='" + Url.Action("NewPassword", "Account", new { email = model.email }, "https") + "'>Reset Password</a>";
                    string subject = "Reset Password";
                    string body = "<b>Please find the Password Reset Link. </b><br/><br/>" + lnkHref;
                    List<string> toList = new List<string>();
                    toList.Add(model.email);
                    EmailManager.SendEmail(toList, subject, body);
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>An email has been sent to your account. <b>Click on the link in received email to reset the password.</b><button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("email", "Enter correct email address");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult NewPassword(NewPassword model)
        {
            if (ModelState.IsValid)
            {
                User user = _ciPlatformContext.Users.Where(user => user.Email.Equals(model.email)).FirstOrDefault();
                user.Password = model.password;
                user.UpdatedAt = DateTime.Now;
                _ciPlatformContext.Users.Update(user);
                _ciPlatformContext.SaveChanges();
                return RedirectToAction("MissionListing", "Mission", new { loginPopUp = true });
            }
            else
            {
                return View(model);
            }

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }
    }
}
