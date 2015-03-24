namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class seat1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Seats");
            AddColumn("dbo.Seats", "SeatId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Seats", "SeatId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Seats");
            DropColumn("dbo.Seats", "SeatId");
            AddPrimaryKey("dbo.Seats", new[] { "Row", "Number" });
        }
    }
}
