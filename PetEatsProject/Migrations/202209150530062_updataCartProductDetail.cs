namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataCartProductDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ProductDetail", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "ProductDetail");
        }
    }
}
