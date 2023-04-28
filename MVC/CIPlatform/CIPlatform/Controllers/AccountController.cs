using CI_PLATFORM_MAIN_ENTITIES.Models.ViewModels;
using CIPlatform.Auth;
using Entities.Auth;
using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Repository.Repository.Interface;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly RegisterInterface _registerInterface;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
      
        public AccountController(ILogger<AccountController> logger, RegisterInterface registerInterface, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
           _registerInterface = registerInterface;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        /// <summary>
        /// View - Login Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            Login login = new Login();
            login.banners = _registerInterface.GetBanners();
            return View(login);
        }

        /// <summary>
        /// View - Forgot Password Page
        /// </summary>
        /// <returns></returns>
        public IActionResult ForgotPassword()
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.banners = _registerInterface.GetBanners();
            return View(forgotPassword);
        }

        /// <summary>
        /// View - Register Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            Register register = new Register();
            register.banners = _registerInterface.GetBanners();
            return View(register);
        }

        /// <summary>
        /// View - Reset Password Page
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
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
            newPasswordModel.banners = _registerInterface.GetBanners();
            return View(newPasswordModel);
        }

        /// <summary>
        /// Allows user to Register on CI - Platform
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(Register user)
        {
            var emailExist = _registerInterface.isEmailAvailable(user.email);
            Register newRegister = new Register();
            newRegister.banners = _registerInterface.GetBanners();

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
            return RedirectToAction("Login", "Account", newRegister);
        }
 
        /// <summary>
        /// Allows Registeres User to Login 
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ValidateLogin(Login N)
        {
            Login login = new Login();
            login.banners = _registerInterface.GetBanners();
            if (ModelState.IsValid)
            {
                var emailExist = _registerInterface.isEmailAvailable(N.email);
                SessionDetailsViewModel sessionDetailsViewModel = new SessionDetailsViewModel();
                if (emailExist)
                {
                    
                    var password = _registerInterface.isPasswordAvailable(N.password, N.email);
                   
                    
                     if(password != null)
                    {
                        sessionDetailsViewModel.Email = password.Email;
                        sessionDetailsViewModel.Avatar = password.Avatar;
                        sessionDetailsViewModel.UserId = password.UserId;
                        sessionDetailsViewModel.FullName = password.FirstName + " " + password.LastName;
                        sessionDetailsViewModel.Role = password.Role;
                        var jwtSetting = _configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
                        var token = JwtTokenHelper.GenerateToken(jwtSetting, sessionDetailsViewModel);
                        if (string.IsNullOrWhiteSpace(token))
                        {
                            ModelState.AddModelError("email", "Enter correct email");
                            return View("Login");
                        }
                       
                        HttpContext.Session.SetString("Token", token);
                        HttpContext.Session.SetString("useremail", N.email);
                        HttpContext.Session.SetString("firstName", password.FirstName);
                        HttpContext.Session.SetString("lastname", password.LastName);
                        HttpContext.Session.SetString("userId", password.UserId.ToString());
                        HttpContext.Session.SetString("avatar", password.Avatar);
                        if (password.Role == "volunteer")
                        {
                            if(password.CountryId != null && password.CityId != null)
                            {
                                return RedirectToAction("MissionListing", "Mission");
                            }
                            else
                            {
                                return RedirectToAction("EditProfile", "Profile");
                            }
                        }
                        if (password.Role == "admin")
                            return RedirectToAction("User", "Admin");

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
                    ModelState.AddModelError("email", "Enter correct email address and also user shouldn't be inactive");
                    return View("Login", login);
                }
            }
            return RedirectToAction("Login", login);
        }

        /// <summary>
        /// User gets a link on their registered Email to reset a password
        /// Link to Reset Password will expire after 4 hours
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword obj)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.banners = _registerInterface.GetBanners();
            if (!_registerInterface.isEmailAvailable(obj.email))
            {
                ModelState.AddModelError("Email", "Email not found");
                return View();

            }
            if(ModelState.IsValid)
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
                MailHelper mailHelper = new MailHelper(_configuration);
                ViewBag.sendMail = mailHelper.Send(obj.email, welcomeMessage + path); 
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>An email has been sent to your account. <b>Click on the link in received email to reset the password.</b><button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View(forgotPassword);
            }
            return View(forgotPassword);
        }

        /// <summary>
        /// Allows user to Reset Password
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NewPassword(NewPassword obj)
        {
            NewPassword newPassword = new NewPassword();
            newPassword.banners = _registerInterface.GetBanners();
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
            return View(newPassword);
        }

        /// <summary>
        /// Logout from CI - Platform
        /// </summary>
        /// <returns></returns>
        public IActionResult logout()
        {
            HttpContext.Session.Remove("useremail");
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Account");
        }
        
    }
}
