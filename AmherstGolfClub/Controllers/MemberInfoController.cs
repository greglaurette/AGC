using AmherstGolfClub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmherstGolfClub.Models;

namespace AmherstGolfClub.Controllers
{


    public class MemberInfoController : Controller
    {
        private GolfContext db = new GolfContext();

        public ActionResult Index()
        {
            var events = db.Events;
            return View(events);
           
        }
       

    }
}

