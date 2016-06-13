using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmherstGolfClub.DAL;
using AmherstGolfClub.Models;

namespace AmherstGolfClub.Controllers
{
    public class KitchenController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: Kitchen
        public ActionResult Index()
        {
            var menuItems = db.MenuItems.Include(m => m.MenuCategory);
            return View(menuItems);
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
