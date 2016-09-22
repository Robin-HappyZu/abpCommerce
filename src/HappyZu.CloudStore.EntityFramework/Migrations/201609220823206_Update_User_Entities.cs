namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_User_Entities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "UnionID", c => c.String());
            AddColumn("dbo.AbpUsers", "WechatOpenID", c => c.String());
            AlterColumn("dbo.Trip_Dest", "Coding", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trip_Dest", "Coding", c => c.String());
            DropColumn("dbo.AbpUsers", "WechatOpenID");
            DropColumn("dbo.AbpUsers", "UnionID");
        }
    }
}
