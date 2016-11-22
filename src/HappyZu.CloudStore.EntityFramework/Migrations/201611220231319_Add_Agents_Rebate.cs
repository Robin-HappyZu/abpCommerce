namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Agents_Rebate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents_Rebate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentId = c.Long(nullable: false),
                        OrderType = c.String(maxLength: 50),
                        OrderNo = c.String(maxLength: 63),
                        UserName = c.String(),
                        PaidTime = c.DateTime(nullable: false),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RebateAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncomeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpectedRebateDate = c.DateTime(nullable: false),
                        RebateDate = c.DateTime(nullable: false),
                        RebateStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Agents_Rebate");
        }
    }
}
