namespace AmherstGolfClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tournaments", "TournamentName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tournaments", "TournamentName", c => c.String());
        }
    }
}
