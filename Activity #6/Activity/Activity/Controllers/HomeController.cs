using Activity.Models;
using Activity.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

        public ActionResult Email()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Email(Message message)
        {
            if (ModelState.IsValid)
            {
                TempData["MSG"] = Functions.SendEmail(message.Email, message.Subject, message.Body);
            }
            else
            {
                TempData["MSG"] = "warning|Fill all fields!";
            }
            return View(message);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = context.User.Where(x => x.Email == forgotPassword.Email).ToList().FirstOrDefault();
                if (user != null)
                {
                    user.Hash = Functions.Encode(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();

                    StringBuilder message = new StringBuilder();
                    message.Append("<h3>System</h3>");
                    message.Append("Change your password by ");
                    message.Append("<a href='http://localhost:54305/Home/ChangePassword/");
                    message.Append(user.Hash).Append("' target='_blank'>clicking here</a>");

                    Functions.SendEmail(user.Email, "Password change", message.ToString());
                    TempData["MSG"] = "success|Email sent. Please, verify your inbox.";
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "error|Email not found!";
                return View();
            }
            TempData["MSG"] = "warning|Fill all fields!";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                Context db = new Context();
                var user = db.User.Where(x => x.Hash == changePassword.Hash).ToList().FirstOrDefault();
                if (user != null)
                {
                    user.Hash = null;
                    user.Password = Functions.HashText(changePassword.Password, "SHA512");
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Your password was changed!";
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "error|Email not found!";
                return View(changePassword);
            }
            TempData["MSG"] = "warning|Fill all fields!";
            return View(changePassword);
        }
    }
}
