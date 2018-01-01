using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TextApp.EntityFrameworkContext;
using TextApp.Models;

namespace TextApp.Controllers
{
    public class TClientsController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();
        MD5 md5Hash = MD5.Create();

        // GET: TClients
        public ActionResult Index()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                return View(db.TClients.ToList());
            }
            else
            {
                return View("AccesDenied");
            }
            
        }

        // GET: TClients/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TClients tClients = db.TClients.Find(id);
                if (tClients == null)
                {
                    return HttpNotFound();
                }
                return View(tClients);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // GET: TClients/Create
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

        // POST: TClients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TClients account)
        {
            if (ModelState.IsValid)
            {
                using (CarsDBEntities db = new CarsDBEntities())
                {
                    var usr = db.TClients.Where(u => u.Login == account.Login).FirstOrDefault();
                    if (usr != null)
                    {
                        ViewBag.BadLogin = "Chosen login is already used";
                        return View();
                    }
                    else
                    {
                        string hash = PasswordHash.GetMd5Hash(md5Hash, account.Password);
                        account.Password = hash;
                        account.Avatar = "~/Images/user.png";
                        db.TClients.Add(account);
                        db.SaveChanges();
                    }
                }
                ModelState.Clear();
            }

            return View();
        }

        // GET: TClients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TClients tClients = db.TClients.Find(id);
                if (tClients == null)
                {
                    return HttpNotFound();
                }
                return View(tClients);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // POST: TClients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TClients tClients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tClients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tClients);
        }

        // GET: TClients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TClients tClients = db.TClients.Find(id);
                if (tClients == null)
                {
                    return HttpNotFound();
                }
                return View(tClients);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // POST: TClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TClients tClients = db.TClients.Find(id);
            db.TClients.Remove(tClients);
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
