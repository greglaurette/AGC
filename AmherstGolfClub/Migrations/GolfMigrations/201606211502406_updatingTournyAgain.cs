namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingTournyAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "FileName");
        }
    }
}
