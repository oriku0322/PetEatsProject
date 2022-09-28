namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataProductName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Products", "ProductClassName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductClassName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Products", "ProductName");
        }
    }
}
