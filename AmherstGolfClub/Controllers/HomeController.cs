using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using AmherstGolfClub.Models;

namespace AmherstGolfClub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactForm c)
        {
            if(ModelState.IsValid)
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
                    smtp.Credentials = new NetworkCredential("justforclass.oulton@gmail.com", "oulton!@#$");    
                    msg.To.Add("greglaurette@gmail.com");
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
                catch(Exception)
                {
                    return View("Error");
                }
                
            }
            return View();
        }
    }
}