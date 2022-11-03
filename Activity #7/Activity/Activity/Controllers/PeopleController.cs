using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Activity.Models;
using Activity.Utils;

namespace Activity.Controllers
{
    public class PeopleController : Controller
    {
        private Context db = new Context();

        // GET: People
        public ActionResult Index()
        {
            return View(db.Person.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Person person, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Person.Add(person);
                db.SaveChanges();
                if (file != null)
                {
                    Functions.CreateDirectory();
                    string fileName = "Photo" + person.Id + ".png";
                    string value = Functions.UploadFile(file, fileName);
                    if (value == "success")
                    {
                        person.Photo = fileName;
                        db.Entry(person).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|User created!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\Photo" + person.Id
                        + ".png");
                        person.Photo = "Photo" + person.Id + ".png";
                        db.Entry(person).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "warning|There was a problem uploading photo: " + value;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\Photo" + person.Id + ".png");
                    person.Photo = "Photo" + person.Id + ".png";
                    db.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|User created!";
                    return RedirectToAction("Index");
                }
            }
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Photo")] Person person, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                if (file != null)
                {
                    Functions.CreateDirectory();
                    string fileName = "Photo" + person.Id + ".png";
                    string value = Functions.UploadFile(file, fileName);
                    if (value == "success")
                    {
                        TempData["MSG"] = "success|User edited successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Functions.DeleteFile(Request.PhysicalApplicationPath + "Uploads\\Photo" + person.Id + ".png");
                        System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\Photo" + person.Id + ".png");
                        TempData["MSG"] = "success|User edited successfully!";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    Functions.DeleteFile(Request.PhysicalApplicationPath + "Uploads\\Photo" + person.Id + ".png");
                    System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\Photo" + person.Id + ".png");
                    TempData["MSG"] = "success|User edited successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(person);
        }

        public ActionResult EditPhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult SavePhoto(int id, HttpPostedFileBase image)
        {
            try
            {
                string fileName = "Photo" + id + ".png";
                string directory = HttpContext.Request.PhysicalApplicationPath + "Uploads\\";
                image.SaveAs(directory + fileName);
                return Json("Ok");
            }
            catch
            {
                return Json("Error");
            }
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Person person = db.Person.Find(id);
                Functions.DeleteFile(Request.PhysicalApplicationPath + "Uploads\\" + person.Photo);
                db.Person.Remove(person);
                db.SaveChanges();
                TempData["MSG"] = "success|User removed!";
            }
            catch
            {
                TempData["MSG"] = "error|Error occurred while trying to remove user.";
            }
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
    }
}
