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
        public DbSet<Events> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Articles> Article { get; set; }

    }
}