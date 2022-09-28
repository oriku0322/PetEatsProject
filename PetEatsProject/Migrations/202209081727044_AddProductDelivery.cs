namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductDelivery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDeliveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductDeliveryName = c.String(nullable: false, maxLength: 50),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductDeliveries");
        }
    }
}
