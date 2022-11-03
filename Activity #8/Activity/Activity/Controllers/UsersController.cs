using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Activity.Models;

namespace Activity.Controllers
{
    public class UsersController : Controller
    {
        private Context db = new Context();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ValidateEmail(string email)
        {
            User user = db.User.Where(t => t.Email == email).FirstOrDefault();
            if (user != null)
            {
                return Json("y");
            }
            return Json("n");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ChangeStatus(string id)
        {
            User user = db.User.Find(Convert.ToInt32(id));
            if (user != null)
            {
                if (user.Status)
                {
                    user.Status = false;
                }
                else
                {
                    user.Status = true;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json(user.Status ? "t" : "f");
            }
            return Json("n");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult DeleteUser(string id)
        {
            try
            {
                User user = db.User.Find(Convert.ToInt32(id));
                if (user != null)
                {
                    db.User.Remove(user);
                    db.SaveChanges();
                    return Json("ok");
                }
                return Json("error");
            }
            catch
            {
                return Json("error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult EditUser(string id, string name, string email)
        {
            try
            {
                User user = db.User.Find(Convert.ToInt32(id));
                if (user != null)
                {
                    user.Name = name;
                    user.Email = email;
                    if (TryValidateModel(user))
                    {
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json("ok");
                    }
                }
                return Json("error");
            }
            catch
            {
                return Json("error");
            }
        }
    }
}
