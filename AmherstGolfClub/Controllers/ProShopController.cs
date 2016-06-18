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
    public class ProShopController : Controller
    {
        private GolfContext db = new GolfContext();

        // GET: ProShop
        public ActionResult Index(string sortOrder, string name)
        {

            //Prepare sort order 
            ViewBag.CurrentSort = sortOrder;//get current sort from UI
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            
            ViewBag.LowPriceSortParm = sortOrder == "lPrice" ? "lPrice_desc" : "lPrice_desc";
            

       

            var products = from s in db.Products select s;

            

            if(name != null){
                ViewBag.FilterTest = name;
            }



            //Apply the sort order 
            switch (sortOrder)
            {

                
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
               
                case "lPrice":
                    products = products.OrderBy(s => s.Price);
                    break;

                case "lPrice_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                
                default:
                    products = products.OrderBy(s => s.Price);
                    break;
            }
          
            
            return View(products.ToList());
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
