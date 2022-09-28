namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCartDeletProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Carts", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ProductID", c => c.Int(nullable: false));
        }
    }
}
