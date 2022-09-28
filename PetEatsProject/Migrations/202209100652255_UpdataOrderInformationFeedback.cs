namespace PetEatsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataOrderInformationFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInformations", "Feedback", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInformations", "Feedback");
        }
    }
}
