namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOrderFKEYGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations");
            DropIndex("dbo.Orders", new[] { "OrderInformationId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Orders", "OrderInformationId");
            AddForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations", "Id", cascadeDelete: true);
        }
    }
}
