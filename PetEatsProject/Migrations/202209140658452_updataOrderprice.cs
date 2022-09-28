namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataOrderprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "Price", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Price");
            DropColumn("dbo.Orders", "ProductName");
        }
    }
}
