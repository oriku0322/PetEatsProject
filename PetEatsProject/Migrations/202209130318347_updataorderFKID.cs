namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataorderFKID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations");
            DropIndex("dbo.Orders", new[] { "OrderInformationId" });
            AlterColumn("dbo.Orders", "OrderInformationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderInformationId");
            AddForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations");
            DropIndex("dbo.Orders", new[] { "OrderInformationId" });
            AlterColumn("dbo.Orders", "OrderInformationId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderInformationId");
            AddForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations", "Id");
        }
    }
}
