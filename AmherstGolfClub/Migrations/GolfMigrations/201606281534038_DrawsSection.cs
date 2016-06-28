namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrawsSection : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => new { t.TouramentDrawID, t.EventsID, t.TournamentID })
                .ForeignKey("dbo.Events", t => t.EventsID, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentID, cascadeDelete: true)
                .Index(t => t.EventsID)
                .Index(t => t.TournamentID);
            
            AddColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID", c => c.Int());
            AddColumn("dbo.Tournaments", "TournamentDraw_EventsID", c => c.Int());
            AddColumn("dbo.Tournaments", "TournamentDraw_TournamentID", c => c.Int());
            CreateIndex("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" });
            AddForeignKey("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" }, "dbo.TournamentDraws", new[] { "TouramentDrawID", "EventsID", "TournamentID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" }, "dbo.TournamentDraws");
            DropForeignKey("dbo.TournamentDraws", "TournamentID", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentDraws", "EventsID", "dbo.Events");
            DropIndex("dbo.TournamentDraws", new[] { "TournamentID" });
            DropIndex("dbo.TournamentDraws", new[] { "EventsID" });
            DropIndex("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID", "TournamentDraw_EventsID", "TournamentDraw_TournamentID" });
            DropColumn("dbo.Tournaments", "TournamentDraw_TournamentID");
            DropColumn("dbo.Tournaments", "TournamentDraw_EventsID");
            DropColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID");
            DropTable("dbo.TournamentDraws");
        }
    }
}
