using System;
using AmherstGolfClub.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AmherstGolfClub.Models;


namespace AmherstGolfClub.Controllers
{
    public class EventController : ApiController
    {
        private GolfContext db = new GolfContext();

        // GET api/Event
        public IQueryable<Events> GetEvents()
        {
            return db.Events;
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
