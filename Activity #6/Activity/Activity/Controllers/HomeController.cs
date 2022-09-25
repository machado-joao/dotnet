using Activity.Models;
using Activity.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Activity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Normal")]
        public ActionResult TestAuthorize()
        {
            return View();
        }

        private Context context = new Context();

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registration registration)
        {
            if (ModelState.IsValid)
            {
                if (context.User.Where(x => x.Email == registration.Email).ToList().Count > 0)
                {
                    ModelState.AddModelError("Email", "Email registered!");
                    return View(registration);
                }
                UserProfile userProfile = new UserProfile();
                userProfile.User = new User();
                userProfile.User.Name = registration.Name;
                userProfile.User.Email = registration.Email;
                userProfile.User.Password = Functions.HashText(registration.Password, "SHA512");
                userProfile.Profile = context.Profile.Find(2);
                if (userProfile.Profile == null)
                {
                    ModelState.AddModelError("Password", "There is no profile for registration!");
                    return View(registration);
                }
                context.UserProfile.Add(userProfile);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registration);
        }

        public ActionResult Access()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Access(Access acess)
        {
            string password = Functions.HashText(acess.Password, "SHA512");
            User user = context.User.Where(x => x.Email == acess.Email && x.Password == password).ToList().FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id + "|" + user.Name, false);
                string permissions = null;

                foreach (UserProfile up in user.UserProfile)
                {
                    permissions += up.Profile.Description + ",";
                }
                permissions = permissions.Substring(0, permissions.Length - 1);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Id + "|" + user.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, permissions);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Login", "User or password wrong!");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
