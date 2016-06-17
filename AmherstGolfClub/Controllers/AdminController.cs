using AmherstGolfClub.DAL;
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}