using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TextApp.EntityFrameworkContext;
using TextApp.Models;

namespace TextApp.Controllers
{
    public class TOrdersController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();

        // GET: TOrders
        public ActionResult Index()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                return View(db.OrderView.ToList());
            }
            else
            {
                return View("AccesDenied");
            }
        }

        public ActionResult OrderHistory()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                return View(db.OrderHistoryView.ToList());
            }
            else
            {
                return View("AccesDenied");
            }
            
        }
        public ActionResult UserOrderHistory()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("user"))
            {
                ViewBag.Login = Session["Login"].ToString();
                return View(db.OrderHistoryView.ToList());
            }
            else
            {
                return View("AccesDenied");
            }

        }
        public ActionResult UserCurrentOrders()
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("user"))
            {
                ViewBag.Login = Session["Login"].ToString();
                return View(db.OrderView.ToList());
            }
            else
            {
                return View("AccesDenied");
            }

        }
        // GET: TOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TOrders tOrders = db.TOrders.Find(id);
                if (tOrders == null)
                {
                    return HttpNotFound();
                }
                return View(tOrders);
            }
            else
            {
                return View("AccesDenied");
            }
        }

        // GET: TOrders/Create
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
        public ActionResult Archive(int? id)
        {
            TOrders _order = db.TOrders.Find(id);
            TOrderHistory order = new TOrderHistory();
            order.AdditionalInformations = _order.AdditionalInformations;
            order.AmountOfOrder = _order.AmountOfOrder;
            order.Car_id = _order.Car_id;
            order.Client_id = _order.Client_id;
            order.DataEnd = _order.DataEnd;
            order.DataStart = _order.DataStart;
            db.TOrderHistory.Add(order);
            db.TOrders.Remove(_order);
            db.SaveChanges();

            return View("Index", db.TOrders.ToList());
        }

        // POST: TOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TOrders tOrders)
        {
            if (ModelState.IsValid)
            {
                db.TOrders.Add(tOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tOrders);
        }

        // GET: TOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TOrders tOrders = db.TOrders.Find(id);
                if (tOrders == null)
                {
                    return HttpNotFound();
                }
                return View(tOrders);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // POST: TOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TOrders tOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tOrders);
        }

        // GET: TOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserFunc"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TOrders tOrders = db.TOrders.Find(id);
                if (tOrders == null)
                {
                    return HttpNotFound();
                }
                return View(tOrders);
            }
            else
            {
                return View("AccesDenied");
            }

        }

        // POST: TOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TOrders tOrders = db.TOrders.Find(id);
            db.TOrders.Remove(tOrders);
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
