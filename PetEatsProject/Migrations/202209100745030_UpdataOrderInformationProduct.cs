namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataOrderInformationProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderInformations", "ProductsId", "dbo.Products");
            DropIndex("dbo.OrderInformations", new[] { "ProductsId" });
            DropColumn("dbo.OrderInformations", "ProductsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderInformations", "ProductsId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderInformations", "ProductsId");
            AddForeignKey("dbo.OrderInformations", "ProductsId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
