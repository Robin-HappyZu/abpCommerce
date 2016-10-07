namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_E_Ticket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trip_ETicket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        TicketOrderId = c.Int(nullable: false),
                        TicketorderItemId = c.Int(nullable: false),
                        SerialNo = c.Long(nullable: false),
                        Hash = c.String(),
                        Description = c.String(),
                        CreateOn = c.DateTime(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                        CheckedOn = c.DateTime(nullable: false),
                        CheckerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trip_ETicket");
        }
    }
}
