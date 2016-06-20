namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdStartonEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Start", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Start");
        }
    }
}
