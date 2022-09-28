namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCartProductFKEY : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ProductsId", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductsId" });
            DropColumn("dbo.Carts", "ProductsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ProductsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ProductsId");
            AddForeignKey("dbo.Carts", "ProductsId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
