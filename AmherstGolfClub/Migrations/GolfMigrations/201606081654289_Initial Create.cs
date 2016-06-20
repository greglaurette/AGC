namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventsID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        start = c.DateTime(nullable: false),
                        end = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventsID)
                .ForeignKey("dbo.EventTypes", t => t.Type, cascadeDelete: true)
                .Index(t => t.Type);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        EventTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventTypeID);
            
            CreateTable(
                "dbo.TournamentPlayers",
                c => new
                    {
                        TournamentPlayersID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 60),
                        LastName = c.String(nullable: false, maxLength: 60),
                        Club = c.String(nullable: false),
                        Division = c.String(nullable: false),
                        DayOne = c.Int(nullable: false),
                        DayTwo = c.Int(nullable: false),
                        Tournament_TournamentID = c.Int(),
                    })
                .PrimaryKey(t => t.TournamentPlayersID)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_TournamentID)
                .Index(t => t.Tournament_TournamentID);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        TournamentID = c.Int(nullable: false, identity: true),
                        TournamentName = c.String(nullable: false),
                        TournamentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TournamentID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SubDepartment = c.String(),
                        ItemCategory = c.String(),
                        Vendor = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        RatesID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeFrame = c.String(),
                    })
                .PrimaryKey(t => t.RatesID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentPlayers", "Tournament_TournamentID", "dbo.Tournaments");
            DropForeignKey("dbo.Events", "Type", "dbo.EventTypes");
            DropIndex("dbo.TournamentPlayers", new[] { "Tournament_TournamentID" });
            DropIndex("dbo.Events", new[] { "Type" });
            DropTable("dbo.Rates");
            DropTable("dbo.Products");
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentPlayers");
            DropTable("dbo.EventTypes");
            DropTable("dbo.Events");
        }
    }
}
