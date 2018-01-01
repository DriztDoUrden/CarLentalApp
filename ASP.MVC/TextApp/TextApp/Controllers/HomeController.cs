using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TextApp.EntityFrameworkContext;
using System.Threading;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Text;
using TextApp.Models;

namespace TextApp.Controllers
{
    public class HomeController : Controller
    {
        MD5 md5Hash = MD5.Create();
        public ActionResult Index()
        {
            //return RedirectToAction("Login");
            using (CarsDBEntities db = new CarsDBEntities())
            {
                return View(db.TCars.ToList());
            }
        }
        public ActionResult LogOut()
        {
            Session["UserID"] = "";
            Session["Login"] = "";
            Session["UserFunc"] = "";
            Session["UserImg"] = "";


            using (CarsDBEntities db = new CarsDBEntities())
            {
                return View("Index",db.TCars.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TClients account)
        {
            if (ModelState.IsValid)
            {
                using (CarsDBEntities db = new CarsDBEntities())
                {
                    var usr = db.TClients.Where(u => u.Login == account.Login).FirstOrDefault();
                    if(usr!=null)
                    {
                        ViewBag.BadLogin = "Chosen login is already used";
                        return View();
                    }
                    else
                    {
                        string hash =  PasswordHash.GetMd5Hash(md5Hash, account.Password);
                        account.Password = hash;
                        account.Avatar = "~/Images/user.png";
                        ViewBag.RegisterOK = "Register complete!";
                        db.TClients.Add(account);
                        db.SaveChanges();
                    }
                }
                ModelState.Clear();
            }
            return View();
        }

        // Logowanie
        public ActionResult Login()
        {

            Session["LogError"] = "ok";
            return View();
        }

        [HttpPost]
        public ActionResult Login(TClients user)
        {
            using (CarsDBEntities db = new CarsDBEntities())
            {
                var TempUsr = db.TClients.Where(u => u.Login == user.Login).FirstOrDefault();
                var verify = PasswordHash.VerifyMd5Hash(md5Hash, user.Password, TempUsr.Password);
                var usr = db.TClients.Where(u => u.Login == user.Login).FirstOrDefault();
                if (usr != null && verify == true)
                {
                    Session["UserID"] = usr.Id.ToString();
                    Session["Login"] = usr.Login.ToString();
                    Session["UserFunc"] = usr.Function.ToString();
                    Session["UserName"] = usr.Name.ToString();
                    Session["UserSurname"] = usr.Surname.ToString();
                    Session["UserPhone"] = usr.Telephone.ToString();
                    Session["UserEmail"] = usr.Email.ToString();
                    Session["UserImg"] = usr.Avatar.ToString();
                    Session["Flag"] = "abc";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.LogError = "Incorrect login or password";
                    return View();
                }
            }
            
        }

        public ActionResult UserPanel()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult AdminPanel()
        {
            if (Session["UserID"] != null && Session["UserFunc"].ToString().Equals("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


    }
}