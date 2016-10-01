namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ticket_Entity3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trip_Ticket", "FrontMoneyPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trip_Ticket", "FrontMoneyPrice", c => c.Boolean(nullable: false));
        }
    }
}
