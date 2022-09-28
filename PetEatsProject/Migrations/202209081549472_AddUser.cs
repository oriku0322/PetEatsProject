namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        HashPassword = c.String(nullable: false, maxLength: 100),
                        Salt = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        MobilePhone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        UserImage = c.String(),
                        AccountState = c.Boolean(nullable: false),
                        CreatDate = c.DateTime(nullable: false),
                        CheckMailCode = c.String(maxLength: 50),
                        MailCodeCreatDate = c.DateTime(),
                        RefreshToken = c.String(maxLength: 50),
                        RefreshTokenCreatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
