namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Trip_Entities : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TicketQuote_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_TicketQuotes_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TicketQuote_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_TicketQuotes_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
