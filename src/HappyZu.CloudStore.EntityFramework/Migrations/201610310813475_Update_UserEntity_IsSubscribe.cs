namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UserEntity_IsSubscribe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "IsSubscribe", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "IsSubscribe");
        }
    }
}
