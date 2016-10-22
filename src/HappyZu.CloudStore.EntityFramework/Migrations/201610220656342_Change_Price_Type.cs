namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Price_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trip_Dest", "MinPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Dest", "MaxPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Ticket", "MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Ticket", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Ticket", "CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Ticket", "AgentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Ticket", "FrontMoneyPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TicketCollectingPerson", "Insurance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_Traveler", "InsurancePremium", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "AdultPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "ChildPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "OldManPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "SingleSupplement", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "InsurancePremium", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelOrder", "PaidAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Trip_TravelQuotes", "SingleSupplement", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trip_TravelQuotes", "SingleSupplement", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "PaidAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "TotalAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "InsurancePremium", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "SingleSupplement", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "OldManPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "ChildPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TravelOrder", "AdultPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Traveler", "InsurancePremium", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_TicketCollectingPerson", "Insurance", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "FrontMoneyPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "AgentPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "CostPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Ticket", "MarketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Dest", "MaxPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Trip_Dest", "MinPrice", c => c.Double(nullable: false));
        }
    }
}
