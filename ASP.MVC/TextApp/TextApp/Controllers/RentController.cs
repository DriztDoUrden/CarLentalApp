using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TextApp.EntityFrameworkContext;
using TextApp.Models;

namespace TextApp.Controllers
{
    public class RentController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();
        FinalOrder SingleOrder = new FinalOrder();
        public delegate void Sender(MailMessage mail);

        public ActionResult Order(TCars car)
        {
            SingleOrder.car = car;
            return View(SingleOrder);
        }
        [HttpPost]
        public ActionResult Order(FinalOrder _order)
        {
            var original = db.TCars.Find(_order.car.Id);
            if (original.NumberOf == 1)
            {
                original.NumberOf--;
                original.IsAvaiable = false;
            }
            else
            {
                original.NumberOf--;
            }
            db.SaveChanges();
            TOrders order = new TOrders()
            {
                Car_id = _order.car.Id,
                Client_id = Int32.Parse(Session["UserID"].ToString()),
                AdditionalInformations = _order.Text,
                DataStart = _order.DateStart,
                DataEnd = _order.DateEnd,
                AmountOfOrder = _order.TotalPrice
            };
            TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            order.DataStart += ts;
            TimeSpan ts2 = new TimeSpan(23,59,59);
            order.DataEnd += ts2;
            db.TOrders.Add(order);
            db.SaveChanges();

            // sending email
            SendEmail(order);

            return View("SumaryOrder",_order);
        }

        void SendEmail(TOrders EmailOrder)
        {
            OrderPDF pdf = new OrderPDF();
            pdf.CreateDocument(EmailOrder);
            //Configuring webMail class to send emails  
            //gmail smtp server
            var Unit = db.TClients.Find(EmailOrder.Client_id);

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("RentalApp@BD2.pl");
            mail.To.Add(Unit.Email);
            mail.Subject = "Rental App - Your order";
            mail.Body = EmailOrder.AdditionalInformations;

            Attachment attachment = new Attachment(@"D:\ASP.MVC\TextApp\TextApp\Mails\order.pdf");
            mail.Attachments.Add(attachment);

            // TODO, move passwords to the file
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("rentalapppwr@gmail.com", "jabada55555");
            SmtpServer.EnableSsl = true;

            // mail sender
            //Thread SendThread = new Thread(() => SmtpServer.SendMailAsync(mail));
            //SendThread.Start();

            ViewBag.Status = "Email Sent Successfully.";
            //pdf.RemoveDocument();

        }
    }
}