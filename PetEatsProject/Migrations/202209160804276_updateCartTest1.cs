namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCartTest1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderInformationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderInformationId", c => c.Int());
        }
    }
}
