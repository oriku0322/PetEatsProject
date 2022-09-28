namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShopId = c.Int(nullable: false),
                        ProductClassId = c.Int(nullable: false),
                        ProductClassName = c.String(nullable: false, maxLength: 50),
                        Prices = c.String(nullable: false, maxLength: 100),
                        Image = c.String(maxLength: 200),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductClasses", t => t.ProductClassId, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.ProductClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Products", "ProductClassId", "dbo.ProductClasses");
            DropIndex("dbo.Products", new[] { "ProductClassId" });
            DropIndex("dbo.Products", new[] { "ShopId" });
            DropTable("dbo.Products");
        }
    }
}
