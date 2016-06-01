using AmherstGolfClub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            return View();
        }
    }
}