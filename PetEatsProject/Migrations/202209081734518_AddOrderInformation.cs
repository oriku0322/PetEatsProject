namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductsId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        ProductDeliveryId = c.Int(nullable: false),
                        Amount = c.String(nullable: false, maxLength: 50),
                        Memo = c.String(maxLength: 100),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.ProductDeliveries", t => t.ProductDeliveryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductsId)
                .Index(t => t.OrderStatusId)
                .Index(t => t.PaymentId)
                .Index(t => t.ProductDeliveryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderInformations", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderInformations", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.OrderInformations", "ProductDeliveryId", "dbo.ProductDeliveries");
            DropForeignKey("dbo.OrderInformations", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.OrderInformations", "OrderStatusId", "dbo.OrderStatus");
            DropIndex("dbo.OrderInformations", new[] { "ProductDeliveryId" });
            DropIndex("dbo.OrderInformations", new[] { "PaymentId" });
            DropIndex("dbo.OrderInformations", new[] { "OrderStatusId" });
            DropIndex("dbo.OrderInformations", new[] { "ProductsId" });
            DropIndex("dbo.OrderInformations", new[] { "UserId" });
            DropTable("dbo.OrderInformations");
        }
    }
}
