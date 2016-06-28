namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revert0628 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TournamentDraws", "EventsID", "dbo.Events");
            DropForeignKey("dbo.TournamentDraws", "TournamentID", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" }, "dbo.TournamentDraws");
            DropIndex("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" });
            DropIndex("dbo.TournamentDraws", new[] { "EventsID" });
            DropIndex("dbo.TournamentDraws", new[] { "TournamentID" });
            DropColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID");
            DropColumn("dbo.Tournaments", "TournamentDraw_EventsID");
            DropColumn("dbo.Tournaments", "TournamentDraw_TournamentID");
            DropTable("dbo.TournamentDraws");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TournamentDraws",
                c => new
                    {
                        TouramentDrawID = c.Int(nullable: false),
                        EventsID = c.Int(nullable: false),
                        TournamentID = c.Int(nullable: false),
                        TeeTime = c.String(),
                        GolfOne = c.String(),
                        GolfTwo = c.String(),
                        GolfThree = c.String(),
                        GolfFour = c.String(),
                    })
                .PrimaryKey(t => new { t.TouramentDrawID, t.EventsID, t.TournamentID });
            
            AddColumn("dbo.Tournaments", "TournamentDraw_TournamentID", c => c.Int());
            AddColumn("dbo.Tournaments", "TournamentDraw_EventsID", c => c.Int());
            AddColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID", c => c.Int());
            CreateIndex("dbo.TournamentDraws", "TournamentID");
            CreateIndex("dbo.TournamentDraws", "EventsID");
            CreateIndex("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" });
            AddForeignKey("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" }, "dbo.TournamentDraws", new[] { "TouramentDrawID", "EventsID", "TournamentID" });
            AddForeignKey("dbo.TournamentDraws", "TournamentID", "dbo.Tournaments", "TournamentID", cascadeDelete: true);
            AddForeignKey("dbo.TournamentDraws", "EventsID", "dbo.Events", "EventsID", cascadeDelete: true);
        }
    }
}
