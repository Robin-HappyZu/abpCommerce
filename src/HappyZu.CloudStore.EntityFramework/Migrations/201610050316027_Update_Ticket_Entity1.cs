namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ticket_Entity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_Ticket", "Name", c => c.String(maxLength: 255));
            AddColumn("dbo.Trip_Ticket", "MustAdvance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trip_Ticket", "AdvanceBookingDays", c => c.Int(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "FrontMoneyPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Trip_Ticket", "AdvancedBookingDays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trip_Ticket", "AdvancedBookingDays", c => c.Int(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "FrontMoneyPrice", c => c.Boolean(nullable: false));
            DropColumn("dbo.Trip_Ticket", "AdvanceBookingDays");
            DropColumn("dbo.Trip_Ticket", "MustAdvance");
            DropColumn("dbo.Trip_Ticket", "Name");
        }
    }
}