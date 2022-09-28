namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataOrderInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Amount", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "Price", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
