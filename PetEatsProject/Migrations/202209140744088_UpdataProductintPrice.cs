namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataProductintPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Prices");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Prices", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Products", "Price");
        }
    }
}
