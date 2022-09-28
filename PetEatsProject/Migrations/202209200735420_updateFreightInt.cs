namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFreightInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shops", "Freight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shops", "Freight", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
