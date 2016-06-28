namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeIcollect : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tournaments", "TournamentDraw_TouramentDrawID", "dbo.TournamentDraws");
            DropIndex("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID" });
            DropColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID", c => c.Int());
            CreateIndex("dbo.Tournaments", "TournamentDraw_TouramentDrawID");
            AddForeignKey("dbo.Tournaments", "TournamentDraw_TouramentDrawID", "dbo.TournamentDraws", "TouramentDrawID");
        }
    }
}
