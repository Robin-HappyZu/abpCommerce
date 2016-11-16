namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TicketStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_TicketOrder", "TicketStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip_TicketOrder", "TicketStatus");
        }
    }
}
