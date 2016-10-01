namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ticket_Entity2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_Ticket", "Name", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip_Ticket", "Name");
        }
    }
}
