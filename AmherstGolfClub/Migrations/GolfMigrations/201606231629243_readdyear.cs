namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdyear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "Year", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "Year");
        }
    }
}
