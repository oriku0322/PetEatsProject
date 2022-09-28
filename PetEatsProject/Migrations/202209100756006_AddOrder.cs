namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductClassId", "dbo.ProductClasses");
            DropIndex("dbo.Products", new[] { "ProductClassId" });
            RenameColumn(table: "dbo.Products", name: "ProductClassId", newName: "ProductClass_Id");
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductsId = c.Int(nullable: false),
                        OrderInformationId = c.Int(),
                        Amount = c.String(nullable: false, maxLength: 50),
                        Memo = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderInformations", t => t.OrderInformationId)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .Index(t => t.ProductsId)
                .Index(t => t.OrderInformationId);
            
            AlterColumn("dbo.Products", "ProductClass_Id", c => c.Int());
            CreateIndex("dbo.Products", "ProductClass_Id");
            AddForeignKey("dbo.Products", "ProductClass_Id", "dbo.ProductClasses", "Id");
            DropColumn("dbo.OrderInformations", "Amount");
            DropColumn("dbo.OrderInformations", "Memo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderInformations", "Memo", c => c.String(maxLength: 100));
            AddColumn("dbo.OrderInformations", "Amount", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Products", "ProductClass_Id", "dbo.ProductClasses");
            DropForeignKey("dbo.Orders", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations");
            DropIndex("dbo.Orders", new[] { "OrderInformationId" });
            DropIndex("dbo.Orders", new[] { "ProductsId" });
            DropIndex("dbo.Products", new[] { "ProductClass_Id" });
            AlterColumn("dbo.Products", "ProductClass_Id", c => c.Int(nullable: false));
            DropTable("dbo.Orders");
            RenameColumn(table: "dbo.Products", name: "ProductClass_Id", newName: "ProductClassId");
            CreateIndex("dbo.Products", "ProductClassId");
            AddForeignKey("dbo.Products", "ProductClassId", "dbo.ProductClasses", "Id", cascadeDelete: true);
        }
    }
}
