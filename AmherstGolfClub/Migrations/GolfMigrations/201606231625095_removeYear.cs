namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeYear : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tournaments", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "Year", c => c.String());
        }
    }
}
