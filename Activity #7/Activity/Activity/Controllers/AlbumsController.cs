using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Activity.Models;
using Activity.Utils;

namespace Activity.Controllers
{
    public class AlbumsController : Controller
    {
        private Context db = new Context();

        // GET: Albums
        public ActionResult Index()
        {
            var album = db.Album.Include(a => a.Person);
            return View(album.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId")] Album album, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    Functions.CreateDirectory();
                    foreach (HttpPostedFileBase file in files)
                    {
                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
                        string value = Functions.UploadFile(file, fileName);
                        if (value == "success")
                        {
                            album.Photo = fileName;
                            db.Album.Add(album);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", album.PersonId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", album.PersonId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,Photo")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Name", album.PersonId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Album.Find(id);
            Functions.DeleteFile(Request.PhysicalApplicationPath + "Uploads\\" + album.Photo);
            db.Album.Remove(album);
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
    }
}
