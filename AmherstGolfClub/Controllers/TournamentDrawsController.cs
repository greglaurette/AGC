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
    public class TournamentDrawsController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: TournamentDraws
        public ActionResult Index()
        {
            var tournamentDraws = db.TournamentDraws.Include(t => t.Tournaments);
            return View(tournamentDraws.ToList());
        }

        // GET: TournamentDraws/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentDraw tournamentDraw = db.TournamentDraws.Find(id);
            if (tournamentDraw == null)
            {
                return HttpNotFound();
            }
            return View(tournamentDraw);
        }

        // GET: TournamentDraws/Create
        public ActionResult Create()
        {
            
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID");
            return View();
        }

        // POST: TournamentDraws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TouramentDrawID,TournamentID,TeeTime,GolfOne,GolfTwo,GolfThree,GolfFour")] TournamentDraw tournamentDraw)
        {
            if (ModelState.IsValid)
            {
                db.TournamentDraws.Add(tournamentDraw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", tournamentDraw.TournamentID);
            return View(tournamentDraw);
        }

        // GET: TournamentDraws/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentDraw tournamentDraw = db.TournamentDraws.Find(id);
            if (tournamentDraw == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", tournamentDraw.TournamentID);
            return View(tournamentDraw);
        }

        // POST: TournamentDraws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TouramentDrawID,TournamentID,TeeTime,GolfOne,GolfTwo,GolfThree,GolfFour")] TournamentDraw tournamentDraw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournamentDraw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", tournamentDraw.TournamentID);
            return View(tournamentDraw);
        }

        // GET: TournamentDraws/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentDraw tournamentDraw = db.TournamentDraws.Find(id);
            if (tournamentDraw == null)
            {
                return HttpNotFound();
            }
            return View(tournamentDraw);
        }

        // POST: TournamentDraws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TournamentDraw tournamentDraw = db.TournamentDraws.Find(id);
            db.TournamentDraws.Remove(tournamentDraw);
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
