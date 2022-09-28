namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCartTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "CreatDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "CreatDate");
        }
    }
}
