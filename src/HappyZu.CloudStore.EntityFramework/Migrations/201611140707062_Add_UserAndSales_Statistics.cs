namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserAndSales_Statistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics_Sales_ByDay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orders = c.Int(nullable: false),
                        AgentOrders = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_Sales_ByMonth",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orders = c.Int(nullable: false),
                        AgentOrders = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        FirstDayOfMonth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_Sales_ByQuarter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orders = c.Int(nullable: false),
                        AgentOrders = c.Int(nullable: false),
                        Quarter = c.Int(nullable: false),
                        FirstDayOfQuarter = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_Sales_ByWeek",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Orders = c.Int(nullable: false),
                        AgentOrders = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        FirstDayOfWeek = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_User_ByDay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Increase = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_User_ByMonth",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Increase = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        FirstDayOfMonth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_User_ByQuarter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Increase = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Quarter = c.Int(nullable: false),
                        FirstDayOfQuarter = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics_User_ByWeek",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Increase = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        FirstDayOfWeek = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Statistics_User_ByWeek");
            DropTable("dbo.Statistics_User_ByQuarter");
            DropTable("dbo.Statistics_User_ByMonth");
            DropTable("dbo.Statistics_User_ByDay");
            DropTable("dbo.Statistics_Sales_ByWeek");
            DropTable("dbo.Statistics_Sales_ByQuarter");
            DropTable("dbo.Statistics_Sales_ByMonth");
            DropTable("dbo.Statistics_Sales_ByDay");
        }
    }
}
