using AmherstGolfClub.Models;
using AmherstGolfClub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace AmherstGolfClub.Controllers
{
    public class TournamentController : Controller
    {
        GolfContext db = new GolfContext();
        // GET: Tournament
        public ActionResult Index()
        {
            var tourny = db.Tournaments;
            return View(tourny);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tourny = db.Tournaments.Find(id);
            if (tourny == null)
            {
                return HttpNotFound();
            }
            return View(tourny);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TournamentID,TournamentName,TournamentDate")] Tournament tourney)
        {
            if (ModelState.IsValid)
            {
                db.Tournaments.Add(tourney);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tourney);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tourney = db.Tournaments.Find(id);
            if (tourney == null)
            {
                return HttpNotFound();
            }
            return View(tourney);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournament tourney = db.Tournaments.Find(id);
            db.Tournaments.Remove(tourney);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TournamentID,TournamentName,TournamentDate")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

    }
}