namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Trip_Entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trip_City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        ProvinceId = c.Int(nullable: false),
                        DestCount = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DestCity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_Province",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        DestType = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DestProvince_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_Dest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Subject = c.String(maxLength: 255),
                        Feature = c.String(maxLength: 255),
                        DestType = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Address = c.String(maxLength: 255),
                        Lng = c.String(maxLength: 127),
                        Lat = c.String(maxLength: 127),
                        Supplier = c.String(maxLength: 255),
                        SupplierId = c.Int(nullable: false),
                        OpenTime = c.String(maxLength: 255),
                        MinPrice = c.Double(nullable: false),
                        MaxPrice = c.Double(nullable: false),
                        BookingNotice = c.String(),
                        Agreement = c.String(),
                        Introduce = c.String(),
                        CoverImage = c.String(maxLength: 255),
                        IsPublished = c.Boolean(nullable: false),
                        PublishDateTime = c.DateTime(nullable: false),
                        HasTicket = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        MetaTitle = c.String(maxLength: 255),
                        MetaKeywords = c.String(maxLength: 127),
                        MetaDescription = c.String(maxLength: 255),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Dest_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TicketCollectingPerson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        OrderNo = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        PhoneDestType = c.Int(nullable: false),
                        ContryNumber = c.String(maxLength: 15),
                        Mobile = c.String(maxLength: 127),
                        Email = c.String(maxLength: 255),
                        IDType = c.Int(nullable: false),
                        IDNumber = c.String(maxLength: 255),
                        Birthday = c.DateTime(nullable: false),
                        IsInsured = c.Boolean(nullable: false),
                        Insurance = c.Double(nullable: false),
                        InsuranceId = c.Int(nullable: false),
                        IsCollectingTicketsPerson = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TicketOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UseDate = c.DateTime(nullable: false),
                        OrderNo = c.String(maxLength: 128),
                        TicketNo = c.String(maxLength: 128),
                        UsedPoint = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        InsurancePremium = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        Contact = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                        Remark = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        IsVerification = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TicketOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TicketQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Quote_MarketPrice = c.Double(nullable: false),
                        Quote_Price = c.Double(nullable: false),
                        Quote_CostPrice = c.Double(nullable: false),
                        Quote_AgentPrice = c.Double(nullable: false),
                        Quote_Inventory = c.Int(nullable: false),
                        Sales = c.Int(nullable: false),
                        IsDisplay = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TicketQuotes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DestId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        MarketPrice = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        AgentPrice = c.Double(nullable: false),
                        CanPayFrontMoney = c.Boolean(nullable: false),
                        FrontMoneyPrice = c.Boolean(nullable: false),
                        CanUsePoint = c.Boolean(nullable: false),
                        UsePoints = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Inventory = c.Int(nullable: false),
                        Description = c.String(maxLength: 255),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AdvancedBookingDays = c.Int(nullable: false),
                        EndTime = c.Time(nullable: false, precision: 7),
                        QuotesType = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        MetaTitle = c.String(),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Ticket_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TicketType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DestId = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        DisplayOrder = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TicketType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_Traveler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 127),
                        Email = c.String(maxLength: 255),
                        IDType = c.Int(nullable: false),
                        IDNumber = c.String(maxLength: 255),
                        Birthday = c.DateTime(nullable: false),
                        IsInsured = c.Boolean(nullable: false),
                        InsurancePremium = c.Double(nullable: false),
                        InsuranceId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TravelOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        OrderNo = c.String(maxLength: 128),
                        UsedPoint = c.Int(nullable: false),
                        AdultCount = c.Int(nullable: false),
                        AdultPrice = c.Double(nullable: false),
                        ChildCount = c.Int(nullable: false),
                        ChildPrice = c.Double(nullable: false),
                        OldManCount = c.Int(nullable: false),
                        OldManPrice = c.Double(nullable: false),
                        SingleSupplement = c.Double(nullable: false),
                        InsurancePremium = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        Contact = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                        Remark = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TravelOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TravelQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelId = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Quote_MarketPrice = c.Double(nullable: false),
                        Quote_Price = c.Double(nullable: false),
                        Quote_CostPrice = c.Double(nullable: false),
                        Quote_AgentPrice = c.Double(nullable: false),
                        Quote_Inventory = c.Int(nullable: false),
                        ChildQuote_MarketPrice = c.Double(nullable: false),
                        ChildQuote_Price = c.Double(nullable: false),
                        ChildQuote_CostPrice = c.Double(nullable: false),
                        ChildQuote_AgentPrice = c.Double(nullable: false),
                        ChildQuote_Inventory = c.Int(nullable: false),
                        OldManQuote_MarketPrice = c.Double(nullable: false),
                        OldManQuote_Price = c.Double(nullable: false),
                        OldManQuote_CostPrice = c.Double(nullable: false),
                        OldManQuote_AgentPrice = c.Double(nullable: false),
                        OldManQuote_Inventory = c.Int(nullable: false),
                        SingleSupplement = c.Double(nullable: false),
                        Sales = c.Int(nullable: false),
                        IsDisplay = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TravelQuotes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_Travel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Subject = c.String(maxLength: 255),
                        DestType = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        DepartureProvinceId = c.Int(nullable: false),
                        DepartureCityId = c.Int(nullable: false),
                        Supplier = c.String(maxLength: 255),
                        SupplierId = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        Nights = c.Int(nullable: false),
                        AdvancedBookingDays = c.Int(nullable: false),
                        EndTime = c.Time(nullable: false, precision: 7),
                        CanPayFrontMoney = c.Boolean(nullable: false),
                        FrontMoneyPrice = c.Boolean(nullable: false),
                        CanUsePoint = c.Boolean(nullable: false),
                        UsePoints = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        BookingNotice = c.String(),
                        Agreement = c.String(),
                        Introduce = c.String(),
                        Feature = c.String(),
                        Schedule = c.String(),
                        CoverImage = c.String(maxLength: 255),
                        IsPublished = c.Boolean(nullable: false),
                        PublishDateTime = c.DateTime(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        MetaTitle = c.String(maxLength: 255),
                        MetaKeywords = c.String(maxLength: 127),
                        MetaDescription = c.String(maxLength: 255),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Travel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trip_Travel",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Travel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_TravelQuotes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TravelQuotes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_TravelOrder",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TravelOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_Traveler");
            DropTable("dbo.Trip_TicketType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TicketType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_Ticket",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Ticket_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_TicketQuotes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TicketQuotes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_TicketOrder",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TicketOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_TicketCollectingPerson");
            DropTable("dbo.Trip_Dest",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Dest_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_Province",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DestProvince_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_City",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DestCity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
