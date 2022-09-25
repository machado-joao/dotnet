using Activity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Activity.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Client()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Client(string name, string email, string password)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Password = password;
            return View();
        }

        public ActionResult Book()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(Book book)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Book = book;
                return View();
            }
            return View(book);
        }

        public ActionResult Student()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Student(Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Student = student;
                return View();
            }
            return View(student);
        }

        public ActionResult Product()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product(Product product)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Product = product;
                return View();
            }
            return View(product);
        }
    }
}
