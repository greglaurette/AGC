namespace AmherstGolfClub.Migrations.GolfMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuCategories",
                c => new
                    {
                        MenuCategoryID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.MenuCategoryID);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        MenuItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuItemID)
                .ForeignKey("dbo.MenuCategories", t => t.Type, cascadeDelete: true)
                .Index(t => t.Type);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItems", "Type", "dbo.MenuCategories");
            DropIndex("dbo.MenuItems", new[] { "Type" });
            DropTable("dbo.MenuItems");
            DropTable("dbo.MenuCategories");
        }
    }
}
