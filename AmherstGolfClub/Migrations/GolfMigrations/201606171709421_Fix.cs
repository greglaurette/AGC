namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticlesID = c.Int(nullable: false, identity: true),
                        SiteLocation = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ArticlesID);
            
            DropColumn("dbo.Events", "start");
            DropColumn("dbo.Events", "end");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "end", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "start", c => c.DateTime(nullable: false));
            DropTable("dbo.Articles");
        }
    }
}
