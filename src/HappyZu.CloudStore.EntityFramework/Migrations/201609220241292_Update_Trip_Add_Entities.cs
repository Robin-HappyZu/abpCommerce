namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Trip_Add_Entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trip_DestAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_DestAttribute_Record",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DestId = c.Int(nullable: false),
                        DestAttributeId = c.Int(nullable: false),
                        DestAttributeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TicketAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trip_TicketAttribute_Record",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        TicketAttributeId = c.Int(nullable: false),
                        TicketAttributeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Trip_Dest", "Coding", c => c.String());
            AddColumn("dbo.Trip_TicketOrder", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Trip_TicketOrder", "UseDate");
            DropColumn("dbo.Trip_TicketOrder", "TicketNo");
            DropColumn("dbo.Trip_TicketOrder", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trip_TicketOrder", "Amount", c => c.Double(nullable: false));
            AddColumn("dbo.Trip_TicketOrder", "TicketNo", c => c.String(maxLength: 128));
            AddColumn("dbo.Trip_TicketOrder", "UseDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trip_TicketOrder", "CustomerId");
            DropColumn("dbo.Trip_Dest", "Coding");
            DropTable("dbo.Trip_TicketAttribute_Record");
            DropTable("dbo.Trip_TicketAttribute");
            DropTable("dbo.Trip_DestAttribute_Record");
            DropTable("dbo.Trip_DestAttribute");
        }
    }
}
