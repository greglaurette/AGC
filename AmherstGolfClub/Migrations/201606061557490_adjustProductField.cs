namespace AmherstGolfClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjustProductField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Vendor", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Quantity", c => c.String());
            AlterColumn("dbo.Products", "Price", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 60));
            DropColumn("dbo.Products", "Vendor");
        }
    }
}
