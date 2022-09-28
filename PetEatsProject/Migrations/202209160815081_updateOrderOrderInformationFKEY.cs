namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderOrderInformationFKEY : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderCode", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderInformationId");
            AddForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderInformationId", "dbo.OrderInformations");
            DropIndex("dbo.Orders", new[] { "OrderInformationId" });
            DropColumn("dbo.Orders", "OrderCode");
        }
    }
}
