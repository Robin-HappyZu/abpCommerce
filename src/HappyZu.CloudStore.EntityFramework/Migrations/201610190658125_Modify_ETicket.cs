namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_ETicket : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Trip_ETicket", "TicketId");
            AddForeignKey("dbo.Trip_ETicket", "TicketId", "dbo.Trip_Ticket", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trip_ETicket", "TicketId", "dbo.Trip_Ticket");
            DropIndex("dbo.Trip_ETicket", new[] { "TicketId" });
        }
    }
}
