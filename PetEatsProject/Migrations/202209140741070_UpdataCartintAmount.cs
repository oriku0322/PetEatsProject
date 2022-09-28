namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataCartintAmount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carts", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "Amount", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
