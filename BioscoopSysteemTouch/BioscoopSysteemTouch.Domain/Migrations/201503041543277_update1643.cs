namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class update1643 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pegis", "Violence", c => c.Boolean(nullable: false));
            DropColumn("dbo.Pegis", "Vioence");
            DropColumn("dbo.Tickets", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Pegis", "Vioence", c => c.Boolean(nullable: false));
            DropColumn("dbo.Pegis", "Violence");
        }
    }
}
