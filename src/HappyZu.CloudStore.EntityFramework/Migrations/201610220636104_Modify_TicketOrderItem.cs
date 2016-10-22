namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TicketOrderItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trip_TicketOrderItem", "TicketId", "dbo.Trip_Ticket");
            DropIndex("dbo.Trip_TicketOrderItem", new[] { "TicketId" });
            AddColumn("dbo.Trip_TicketOrderItem", "TicketName", c => c.String());
            AddColumn("dbo.Trip_TicketOrderItem", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Trip_TicketOrder", "CustomerId", c => c.Long(nullable: false));
            AlterColumn("dbo.Trip_TicketOrder", "InsurancePremium", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketOrder", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketOrder", "PaidAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_AgentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_AgentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_AgentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_AgentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_AgentPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_CostPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "OldManQuote_MarketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_AgentPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_CostPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "ChildQuote_MarketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_AgentPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_CostPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelQuotes", "Quote_MarketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_AgentPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_CostPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketQuotes", "Quote_MarketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketOrder", "PaidAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketOrder", "TotalAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketOrder", "InsurancePremium", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketOrder", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Trip_TicketOrderItem", "Date");
            DropColumn("dbo.Trip_TicketOrderItem", "TicketName");
            CreateIndex("dbo.Trip_TicketOrderItem", "TicketId");
            AddForeignKey("dbo.Trip_TicketOrderItem", "TicketId", "dbo.Trip_Ticket", "Id", cascadeDelete: true);
        }
    }
}
