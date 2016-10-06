namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Picture_Module : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trip_Dest_Picture_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DestId = c.Int(nullable: false),
                        FileId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UploadFile_FileItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(maxLength: 255),
                        MimeType = c.String(maxLength: 50),
                        SEOFileName = c.String(maxLength: 255),
                        AltAttribute = c.String(maxLength: 255),
                        TitleAttribute = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UploadFile_FileItem");
            DropTable("dbo.Trip_Dest_Picture_Mapping");
        }
    }
}
