namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdataNickname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Nickname", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Nickname");
        }
    }
}
