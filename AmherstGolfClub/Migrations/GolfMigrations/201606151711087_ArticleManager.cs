namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleManager : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Articles");
        }
    }
}
