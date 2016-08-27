namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Create_FAQ_Entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQ_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                        FontIcon = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FAQCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FAQ_Detail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        CategoryId = c.Int(nullable: false),
                        Discription = c.String(),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FAQDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FAQ_Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FAQ_Detail", "CategoryId", "dbo.FAQ_Category");
            DropIndex("dbo.FAQ_Detail", new[] { "CategoryId" });
            DropTable("dbo.FAQ_Detail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FAQDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.FAQ_Category",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FAQCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
