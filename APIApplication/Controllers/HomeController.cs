using APIApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly APIAPPContext context;

        public HomeController(APIAPPContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserProfile user)
        {
            var myUser = context.UserProfiles.FirstOrDefault(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password);
            if (myUser != null)
            {
                HttpContext.Session.SetString("EmailSession", myUser.EmailAddress);
                HttpContext.Session.SetString("PasswordSession", myUser.Password);
                HttpContext.Session.SetString("UserIdSession", myUser.UserId.ToString()); // Convert UserId to string
                HttpContext.Session.SetString("UserNameSession", myUser.UserName.ToString()); // Convert UserId to string
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Login Failed! Please try again";
                return View();
            }
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                ViewBag.EmailSession = HttpContext.Session.GetString("EmailSession").ToString();
                ViewBag.PasswordSession = HttpContext.Session.GetString("PasswordSession").ToString();
                ViewBag.UserIdSession = HttpContext.Session.GetString("UserIdSession").ToString();
                ViewBag.UserNameSession = HttpContext.Session.GetString("UserNameSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [AllowAnonymous]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                HttpContext.Session.Remove("EmailSession");
                HttpContext.Session.Remove("PasswordSession");
                HttpContext.Session.Remove("UserIdSession");
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}