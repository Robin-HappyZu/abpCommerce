namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TicketOrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_TicketOrderItem", "AgentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip_TicketOrderItem", "AgentPrice");
        }
    }
}
