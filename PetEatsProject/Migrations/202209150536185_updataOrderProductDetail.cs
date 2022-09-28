namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataOrderProductDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductDetail", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ProductDetail");
        }
    }
}
