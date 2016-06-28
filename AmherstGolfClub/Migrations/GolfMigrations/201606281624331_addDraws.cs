namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDraws : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentDraws",
                c => new
                    {
                        TouramentDrawID = c.Int(nullable: false, identity: true),
                        TournamentID = c.Int(nullable: false),
                        TeeTime = c.String(),
                        GolfOne = c.String(),
                        GolfTwo = c.String(),
                        GolfThree = c.String(),
                        GolfFour = c.String(),
                    })
                .PrimaryKey(t => t.TouramentDrawID)
                .ForeignKey("dbo.Tournaments", t => t.TournamentID, cascadeDelete: true)
                .Index(t => t.TournamentID);
            
            AddColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID", c => c.Int());
            CreateIndex("dbo.Tournaments", "TournamentDraw_TouramentDrawID");
            AddForeignKey("dbo.Tournaments", "TournamentDraw_TouramentDrawID", "dbo.TournamentDraws", "TouramentDrawID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "TournamentDraw_TouramentDrawID", "dbo.TournamentDraws");
            DropForeignKey("dbo.TournamentDraws", "TournamentID", "dbo.Tournaments");
            DropIndex("dbo.TournamentDraws", new[] { "TournamentID" });
            DropIndex("dbo.Tournaments", new[] { "TournamentDraw_TouramentDrawID" });
            DropColumn("dbo.Tournaments", "TournamentDraw_TouramentDrawID");
            DropTable("dbo.TournamentDraws");
        }
    }
}
