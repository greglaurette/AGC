﻿using System;
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
    public class TournamentController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: Tournament
        public ActionResult Index()
        {
            return View(db.Tournaments.ToList());
        }

        public ActionResult TournyDraw(int id)
        {
            
            var tourny = db.TournamentDraws.Where(i => i.TournamentID == id).ToList().OrderBy(i=>i.TeeTime);
            return View(tourny);
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