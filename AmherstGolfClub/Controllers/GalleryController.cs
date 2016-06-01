using AmherstGolfClub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmherstGolfClub.Controllers
{
    public class GalleryController : Controller
    {
        private GolfContext db = new GolfContext();

        public ActionResult Index()
        {
            return View();
        }
    }
}