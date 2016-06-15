using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class Articles
    {
        public int ArticlesID { get; set; }
        public string SiteLocation { get; set; }
        public string Description { get; set; }

        public IEnumerable<Articles> Article { get; set; }
    }
}