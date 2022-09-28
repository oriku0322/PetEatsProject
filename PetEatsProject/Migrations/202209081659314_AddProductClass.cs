namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductClassName = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 200),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductClasses");
        }
    }
}
