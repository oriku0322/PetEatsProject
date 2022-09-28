namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductClass_Id", "dbo.ProductClasses");
            DropIndex("dbo.Products", new[] { "ProductClass_Id" });
            RenameColumn(table: "dbo.Products", name: "ProductClass_Id", newName: "ProductClassId");
            AlterColumn("dbo.Products", "ProductClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductClassId");
            AddForeignKey("dbo.Products", "ProductClassId", "dbo.ProductClasses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductClassId", "dbo.ProductClasses");
            DropIndex("dbo.Products", new[] { "ProductClassId" });
            AlterColumn("dbo.Products", "ProductClassId", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "ProductClassId", newName: "ProductClass_Id");
            CreateIndex("dbo.Products", "ProductClass_Id");
            AddForeignKey("dbo.Products", "ProductClass_Id", "dbo.ProductClasses", "Id");
        }
    }
}
