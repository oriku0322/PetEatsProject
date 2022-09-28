namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        ShopName = c.String(nullable: false, maxLength: 50),
                        OpeningHourse = c.String(nullable: false, maxLength: 100),
                        ShopTEL = c.String(nullable: false, maxLength: 50),
                        ShopAddress = c.String(nullable: false, maxLength: 100),
                        Freight = c.String(nullable: false, maxLength: 100),
                        Views = c.Int(nullable: false),
                        EvaluateStars = c.Int(nullable: false),
                        Image = c.String(maxLength: 200),
                        CreatDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shops", "CityId", "dbo.Cities");
            DropIndex("dbo.Shops", new[] { "CityId" });
            DropTable("dbo.Shops");
        }
    }
}
