namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrdercancelOrderCode : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "OrderCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderCode", c => c.Int(nullable: false));
        }
    }
}
