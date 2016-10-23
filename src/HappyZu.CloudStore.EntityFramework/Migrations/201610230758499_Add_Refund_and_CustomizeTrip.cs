namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Refund_and_CustomizeTrip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trip_CustomizeTrip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Long(nullable: false),
                        Destination = c.String(),
                        Depart = c.String(),
                        DepartureTime = c.DateTime(nullable: false),
                        Days = c.Int(nullable: false),
                        Contact = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Location = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_RefundRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketOrderId = c.Int(nullable: false),
                        ApplyStatus = c.Int(nullable: false),
                        RefundStatus = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefundRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trip_RefundRecord",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefundRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Trip_CustomizeTrip");
        }
    }
}
