namespace AmherstGolfClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                        TournamentName = c.String(),
                        TournamentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TournamentID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SubDepartment = c.String(nullable: false),
                        ItemCategory = c.String(nullable: false),
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
            DropIndex("dbo.TournamentPlayers", new[] { "Tournament_TournamentID" });
            DropTable("dbo.Rates");
            DropTable("dbo.Products");
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentPlayers");
        }
    }
}
