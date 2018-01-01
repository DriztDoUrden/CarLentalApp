using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TextApp.EntityFrameworkContext;
using System.Windows;
using TextApp.Models;

namespace TextApp.Controllers
{
    public class TCarsController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();
        
        // GET: TCars
        public ActionResult Index()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                return View(db.TCars.ToList());
            }
            else
            {
                return View("AccesDenied");
            }
            
        }

        // GET: TCars/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TCars tCars = db.TCars.Find(id);
                if (tCars == null)
                {
                    return HttpNotFound();
                }
                return View(tCars);
            }
            else
            {
                return View("AccesDenied");
            }
            
        }

        // GET: TCars/Create
        public ActionResult Create()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                return View();
            }
            else
            {
                return View("AccesDenied");
            }
           
        }

        // POST: TCars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TCars tCars)
        {
            if (ModelState.IsValid)
            {
                db.TCars.Add(tCars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tCars);
        }

        // GET: TCars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TCars tCars = db.TCars.Find(id);
                if (tCars == null)
                {
                    return HttpNotFound();
                }
                return View(tCars);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // POST: TCars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TCars tCars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tCars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tCars);
        }

        // GET: TCars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TCars tCars = db.TCars.Find(id);
                if (tCars == null)
                {
                    return HttpNotFound();
                }
                return View(tCars);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // POST: TCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TCars tCars = db.TCars.Find(id);
            db.TCars.Remove(tCars);
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
