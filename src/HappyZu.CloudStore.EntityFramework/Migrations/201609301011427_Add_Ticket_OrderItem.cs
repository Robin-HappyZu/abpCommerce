namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Ticket_OrderItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trip_TicketOrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketOrderItemNo = c.String(),
                        TicketOrderId = c.Int(nullable: false),
                        TicketId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trip_TicketOrderItem");
        }
    }
}
