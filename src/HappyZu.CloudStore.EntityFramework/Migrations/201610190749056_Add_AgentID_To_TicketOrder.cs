namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AgentID_To_TicketOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_TicketOrder", "AgentId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip_TicketOrder", "AgentId");
        }
    }
}
