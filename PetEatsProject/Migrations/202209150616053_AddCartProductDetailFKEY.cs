namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartProductDetailFKEY : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ProductDetailId", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ProductDetailId");
            AddForeignKey("dbo.Carts", "ProductDetailId", "dbo.ProductDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "ProductDetail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ProductDetail", c => c.String(maxLength: 100));
            DropForeignKey("dbo.Carts", "ProductDetailId", "dbo.ProductDetails");
            DropIndex("dbo.Carts", new[] { "ProductDetailId" });
            DropColumn("dbo.Carts", "ProductID");
            DropColumn("dbo.Carts", "ProductDetailId");
        }
    }
}
