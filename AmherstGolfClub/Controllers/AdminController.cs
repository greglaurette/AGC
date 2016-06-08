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
                        product.ItemCategory = csv.GetField<string>(8);
                        product.Vendor = csv.GetField<string>(17);
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
    }
}