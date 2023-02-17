using CIPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
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
        public IActionResult ValidateLogin(Login N)
        {
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
