namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_FAQ_Entities_StringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FAQ_Category", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.FAQ_Category", "Icon", c => c.String(maxLength: 255));
            AlterColumn("dbo.FAQ_Category", "FontIcon", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FAQ_Category", "FontIcon", c => c.String());
            AlterColumn("dbo.FAQ_Category", "Icon", c => c.String());
            AlterColumn("dbo.FAQ_Category", "Name", c => c.String());
        }
    }
}
