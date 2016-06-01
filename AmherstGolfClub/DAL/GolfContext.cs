using AmherstGolfClub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.DAL
{
    public class GolfContext:DbContext
    {
        public GolfContext() : base("DefaultConnection") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rates> Rate { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentPlayers> Players { get; set; }
    }
}