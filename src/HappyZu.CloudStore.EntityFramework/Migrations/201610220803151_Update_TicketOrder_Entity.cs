namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TicketOrder_Entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_TicketOrder", "DestName", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip_TicketOrder", "DestName");
        }
    }
}
