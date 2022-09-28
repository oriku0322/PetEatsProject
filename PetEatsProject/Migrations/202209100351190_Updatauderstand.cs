namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatauderstand : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CreatDate", c => c.DateTime());
            AlterColumn("dbo.Users", "MailCodeCreatDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "MailCodeCreatDate", c => c.DateTime());
            AlterColumn("dbo.Users", "CreatDate", c => c.DateTime(nullable: false));
        }
    }
}
