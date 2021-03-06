﻿using AmherstGolfClub.DAL;
using AmherstGolfClub.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AmherstGolfClub.Controllers
{
    public class AdminController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase CSVName)
        {
            string path = null;
            var ProductsToDisplay = new List<Product>();
            try
            {
                if (CSVName.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(CSVName.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "upload\\" + fileName;
                    CSVName.SaveAs(path);

                    var csv = new CsvReader(new StreamReader(path));
                    var db = new GolfContext();
                    db.Database.ExecuteSqlCommand("delete from Products");
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Products, RESEED, 0);");
                    int count = 0;

                    while (csv.Read())
                    {
                        Product product = new Product();
                        //product.ProductID = count;              
                        product.Name = csv.GetField<string>(0);
                        //product.Price = csv.GetField<string>(1);
                        //product.Quantity = csv.GetField<string>(2);
                        //product.SubDepartment = csv.GetField<string>(3);
                        //product.ItemCategory = csv.GetField<string>(4);
                        product.Price = decimal.Parse(csv.GetField(2));
                        if (csv.GetField<string>(3) == "" || int.Parse(csv.GetField<string>(3)) < 0)
                            product.Quantity = 0;
                        else
                            product.Quantity = int.Parse(csv.GetField<string>(3));
                        product.SubDepartment = csv.GetField<string>(7);
                        if (csv.GetField<string>(8) == "")
                            product.ItemCategory = "No Category";
                        else
                            product.ItemCategory = csv.GetField<string>(8);
                        ProductsToDisplay.Add(product);
                        var exists = db.Products.Where(i => i.ProductID == product.ProductID).SingleOrDefault();
                        if (exists == null)
                        {
                            db.Products.Add(product);
                            db.SaveChanges();
                            count += 1;
                        }
                    }
                    ViewBag.RecordsUploaded = ProductsToDisplay.Count();
                    ViewBag.RecordsSaved = count;
                }
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(ProductsToDisplay);
        }

        public ActionResult ProShopImageUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProShopImageUpload(HttpPostedFileBase ResultFile)
        {
            string path = null;
            try
            {
                if (ResultFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ResultFile.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "images\\" + fileName;
                    ViewBag.Uploaded = "Success";
                    ResultFile.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        public ActionResult ViewMenu()
        {
            var menuItems = db.MenuItems.Include(m => m.MenuCategory);
            return View(menuItems.ToList());
        }
        public ActionResult MenuCreate()
        {
            ViewBag.Type = new SelectList(db.MenuCategories, "MenuCategoryID", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuCreate([Bind(Include = "MenuItemID,ItemName,ItemPrice,Type")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Type = new SelectList(db.MenuCategories, "MenuCategoryID", "Type", menuItem.Type);
            return View(menuItem);
        }
        public ActionResult MenuEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(db.MenuCategories, "MenuCategoryID", "Type", menuItem.Type);
            return View(menuItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuEdit([Bind(Include = "MenuItemID,ItemName,ItemPrice,Type")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type = new SelectList(db.MenuCategories, "MenuCategoryID", "Type", menuItem.Type);
            return View(menuItem);
        }
        public ActionResult MenuDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: Kitchen/Delete/5
        [HttpPost, ActionName("MenuDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult MenuDeleteConfirmed(int id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuItem);
            db.SaveChanges();
            return RedirectToAction("ViewMenu");
        }

        public ActionResult ArticleList()
        {
            var articleList = db.Article;
            return View(articleList.ToList());
        }

        public ActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(db.Article, "SiteLocation", "Description");
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle([Bind(Include = "ArticlesID,SiteLocation,Description")] Articles article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ArticleList");
            }
            ViewBag.Type = new SelectList(db.Article, "SiteLocation", "Description");
            return View(article);
        }

        public ActionResult EventList()
        {
            var events = db.Events.Include(e => e.EventType);
            return View(events.ToList());
        }

        public ActionResult EventCreate()
        {
            ViewBag.Type = new SelectList(db.EventTypes, "EventTypeID", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventCreate([Bind(Include = "EventsID,EventName,Description,Date,start,end,Type")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("EventList");
            }

            ViewBag.Type = new SelectList(db.EventTypes, "EventTypeID", "Type", events.Type);
            return View(events);
        }

        public ActionResult EventEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(db.EventTypes, "EventTypeID", "Type", events.Type);
            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventEdit([Bind(Include = "EventsID,EventName,Description,Date,start,end,Type")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EventList");
            }
            ViewBag.Type = new SelectList(db.EventTypes, "EventTypeID", "Type", events.Type);
            return View(events);
        }


        public ActionResult EventDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("EventDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult EventDeleteConfirmed(int id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
            db.SaveChanges();
            return RedirectToAction("EventList");
        }

        public ActionResult EventUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EventUpload(HttpPostedFileBase CSVName)
        {
            string path = null;
            var EventsToDisplay = new List<Events>();
            try
            {
                if (CSVName.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(CSVName.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "upload\\" + fileName;
                    CSVName.SaveAs(path);

                    var csv = new CsvReader(new StreamReader(path));
                    var db = new GolfContext();
                    db.Database.ExecuteSqlCommand("Delete from Events");
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Events, RESEED, 0);");
                    int count = 0;

                    while (csv.Read())
                    {
                        Events events = new Events();
                        events.EventName = csv.GetField<string>(0);
                        //DateTime dateCheck = csv.GetField<DateTime>(1);
                        //string dateString = dateCheck.ToString("yyyy-MM-dd");
                        //DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);
                        events.Date = csv.GetField<DateTime>(1).Date;
                        string type = csv.GetField<string>(2);
                        switch (type)
                        {
                            case "Tournament Events":
                                events.Type = 1;
                                break;
                            case "Public Events":
                                events.Type = 8;
                                break;
                            case "Member Events":
                                events.Type = 2;
                                break;
                            case "Corporate Events":
                                events.Type = 3;
                                break;
                            case "Wedding Events":
                                events.Type = 6;
                                break;
                            case "Special Events":
                                events.Type = 7;
                                break;
                            case "Demo Days":
                                events.Type = 5;
                                break;
                            default:
                                events.Type = int.Parse(type);
                                break;
                        }
                        events.Start = csv.GetField<string>(3);
                        EventsToDisplay.Add(events);
                        var exists = db.Events.Where(i => i.EventsID == events.EventsID).SingleOrDefault();
                        if (exists == null)
                        {
                            db.Events.Add(events);
                            db.SaveChanges();
                            count += 1;                            
                        }
                    }
                    ViewBag.EventsUploaded = EventsToDisplay.Count();
                    ViewBag.EventsSaved = count;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(EventsToDisplay);
        }
        public ActionResult TournyList()
        {
            var tourny = db.Tournaments.ToList();
            return View(tourny);
        }

        public ActionResult TournyCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TournyCreate([Bind(Include = "TournamentID,TournamentName,TournamentDate,Year,FileName")] Tournament tourney)
        {
            if (ModelState.IsValid)
            {
                db.Tournaments.Add(tourney);
                db.SaveChanges();
                return RedirectToAction("TournyList");
            }

            return View(tourney);
        }

        public ActionResult TournyDelete(int? id)
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
        [HttpPost, ActionName("TournyDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult TournyDeleteConfirmed(int id)
        {
            Tournament tourney = db.Tournaments.Find(id);
            db.Tournaments.Remove(tourney);
            db.SaveChanges();
            return RedirectToAction("TournyList");
        }

        public ActionResult TournyEdit(int? id)
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
        public ActionResult TournyEdit([Bind(Include = "TournamentID,TournamentName,TournamentDate,Year,FileName")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TournyList");
            }
            return View(tournament);
        }
        public ActionResult TournyUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TournyUpload(HttpPostedFileBase ResultFile)
        {
            string path = null;            
            try
            {
                if (ResultFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ResultFile.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "Tournaments\\" + fileName;
                    ViewBag.Uploaded = "Success";
                    ResultFile.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        public ActionResult DrawList()
        {
            var tournamentDraws = db.TournamentDraws.Include(t => t.Tournaments);
            return View(tournamentDraws.ToList());
        }
        public ActionResult DrawCreate()
        {
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName");
            return View();
        }

       public ActionResult DrawUpload()
        {
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DrawCreate([Bind(Include = "TouramentDrawID,TournamentID,TeeTime,GolfOne,GolfTwo,GolfThree,GolfFour")] TournamentDraw tournamentDraw)
        {
            if (ModelState.IsValid)
            {
                db.TournamentDraws.Add(tournamentDraw);
                db.SaveChanges();
                return RedirectToAction("DrawList");
            }

            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName", tournamentDraw.TournamentID);
            return View(tournamentDraw);
        }

        [HttpPost]
        public ActionResult DrawUpload(HttpPostedFileBase CSVName, string TournamentID)
        {
            string path = null;
            var DrawToDisplay = new List<TournamentDraw>();
            try
            {
                if (CSVName.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(CSVName.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "upload\\" + fileName;
                    CSVName.SaveAs(path);

                    var csv = new CsvReader(new StreamReader(path));
                    var db = new GolfContext();
                    int count = 0;

                    while (csv.Read())
                    {
                        TournamentDraw draw = new TournamentDraw();

                        var TID = int.Parse(TournamentID);
                        draw.TournamentID = TID;             
                        draw.TeeTime = csv.GetField<string>(0);
                        draw.GolfOne = csv.GetField<string>(1);
                        draw.GolfTwo = csv.GetField<string>(2);
                        draw.GolfThree = csv.GetField<string>(3);
                        draw.GolfFour = csv.GetField<string>(4);

                        
                        
                        DrawToDisplay.Add(draw);
                        var exists = db.TournamentDraws.Where(i => i.TouramentDrawID == draw.TouramentDrawID).SingleOrDefault();
                        if (exists == null)
                        {
                            db.TournamentDraws.Add(draw);
                            db.SaveChanges();
                            count += 1;
                        }
                    }
                    ViewBag.RecordsUploaded = DrawToDisplay.Count();
                    ViewBag.RecordsSaved = count;
                }
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        // GET: TournamentDraws/Edit/5
        public ActionResult DrawEdit(int? id)
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
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName", tournamentDraw.TournamentID);
            return View(tournamentDraw);
        }

        // POST: TournamentDraws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DrawEdit([Bind(Include = "TouramentDrawID,TournamentID,TeeTime,GolfOne,GolfTwo,GolfThree,GolfFour")] TournamentDraw tournamentDraw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournamentDraw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DrawList");
            }
            ViewBag.TournamentID = new SelectList(db.Tournaments, "TournamentID", "TournamentName", tournamentDraw.TournamentID);
            return View(tournamentDraw);
        }

        // GET: TournamentDraws/Delete/5
        public ActionResult DrawDelete(int? id)
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
        [HttpPost, ActionName("DrawDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TournamentDraw tournamentDraw = db.TournamentDraws.Find(id);
            db.TournamentDraws.Remove(tournamentDraw);
            db.SaveChanges();
            return RedirectToAction("DrawList");
        }
    }
}