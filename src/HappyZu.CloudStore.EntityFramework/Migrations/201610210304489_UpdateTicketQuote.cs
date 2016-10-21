namespace HappyZu.CloudStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicketQuote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip_TicketQuotes", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip_TicketQuotes", "DateTime");
        }
    }
}
