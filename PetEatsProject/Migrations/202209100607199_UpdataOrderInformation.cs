namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataOrderInformation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderInformations", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.OrderInformations", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.OrderInformations", "ProductDeliveryId", "dbo.ProductDeliveries");
            DropIndex("dbo.OrderInformations", new[] { "OrderStatusId" });
            DropIndex("dbo.OrderInformations", new[] { "PaymentId" });
            DropIndex("dbo.OrderInformations", new[] { "ProductDeliveryId" });
            AlterColumn("dbo.OrderInformations", "OrderStatusId", c => c.Int());
            AlterColumn("dbo.OrderInformations", "PaymentId", c => c.Int());
            AlterColumn("dbo.OrderInformations", "ProductDeliveryId", c => c.Int());
            CreateIndex("dbo.OrderInformations", "OrderStatusId");
            CreateIndex("dbo.OrderInformations", "PaymentId");
            CreateIndex("dbo.OrderInformations", "ProductDeliveryId");
            AddForeignKey("dbo.OrderInformations", "OrderStatusId", "dbo.OrderStatus", "Id");
            AddForeignKey("dbo.OrderInformations", "PaymentId", "dbo.Payments", "Id");
            AddForeignKey("dbo.OrderInformations", "ProductDeliveryId", "dbo.ProductDeliveries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderInformations", "ProductDeliveryId", "dbo.ProductDeliveries");
            DropForeignKey("dbo.OrderInformations", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.OrderInformations", "OrderStatusId", "dbo.OrderStatus");
            DropIndex("dbo.OrderInformations", new[] { "ProductDeliveryId" });
            DropIndex("dbo.OrderInformations", new[] { "PaymentId" });
            DropIndex("dbo.OrderInformations", new[] { "OrderStatusId" });
            AlterColumn("dbo.OrderInformations", "ProductDeliveryId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderInformations", "PaymentId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderInformations", "OrderStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderInformations", "ProductDeliveryId");
            CreateIndex("dbo.OrderInformations", "PaymentId");
            CreateIndex("dbo.OrderInformations", "OrderStatusId");
            AddForeignKey("dbo.OrderInformations", "ProductDeliveryId", "dbo.ProductDeliveries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderInformations", "PaymentId", "dbo.Payments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderInformations", "OrderStatusId", "dbo.OrderStatus", "Id", cascadeDelete: true);
        }
    }
}
