namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TokenLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RefreshToken = c.String(maxLength: 50),
                        CreatDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TokenLogs", "UserId", "dbo.Users");
            DropIndex("dbo.TokenLogs", new[] { "UserId" });
            DropTable("dbo.TokenLogs");
        }
    }
}
