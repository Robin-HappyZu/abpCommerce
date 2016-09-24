namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ticket_Entity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trip_Ticket", "MetaTitle");
            DropColumn("dbo.Trip_Ticket", "MetaKeywords");
            DropColumn("dbo.Trip_Ticket", "MetaDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trip_Ticket", "MetaDescription", c => c.String());
            AddColumn("dbo.Trip_Ticket", "MetaKeywords", c => c.String());
            AddColumn("dbo.Trip_Ticket", "MetaTitle", c => c.String());
        }
    }
}
