namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjustTourny : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "Year", c => c.String());
            DropColumn("dbo.Tournaments", "TournyYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "TournyYear", c => c.Int(nullable: false));
            DropColumn("dbo.Tournaments", "Year");
        }
    }
}
