using AmherstGolfClub.DAL;
using AmherstGolfClub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AmherstGolfClub.Controllers
{
    public class RatesController : Controller
    {
        private GolfContext db = new GolfContext();

        public ActionResult ShopRentals()
        {
            return View(db.Rate.ToList());
        }
        public ActionResult GeneralRates()
        {
            return View(db.Rate.ToList());
        }
        public ActionResult Membership()
        {
            return View(db.Rate.ToList());
        }
        public ActionResult Corp()
        {
            return View(db.Rate.ToList());
        }

        public ActionResult Events()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Events(ContactForm c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress(c.emailAddress.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.IsBodyHtml = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("agcc1909@gmail.com", "#foxRanch3");
                    msg.To.Add("agcc@eastlink.ca");
                    msg.From = from;
                    msg.Subject = c.emailSubject;
                    sb.Append("Name: " + c.emailName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Message: " + c.emailMessage);
                    msg.Body = sb.ToString();
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success");

                }
                catch (Exception)
                {
                    return View("Error");
                }

            }
            return View();
        }
    }
}