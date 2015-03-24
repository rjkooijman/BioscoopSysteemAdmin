namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class Database_update_04032015_1117 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        TicketSoort = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TicketId);
            
            AddColumn("dbo.Orders", "ticket_TicketId", c => c.Int());
            AddColumn("dbo.Seats", "Show_ShowID", c => c.Int());
            AddColumn("dbo.Shows", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movies", "ImageData", c => c.String());
            AlterColumn("dbo.Pegis", "All", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "SixPlus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "TwelvePlus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "SixteenPlus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "Vioence", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "Horror", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "Sex", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "Language", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "Drugs", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pegis", "Racism", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Orders", "ticket_TicketId");
            CreateIndex("dbo.Seats", "Show_ShowID");
            AddForeignKey("dbo.Seats", "Show_ShowID", "dbo.Shows", "ShowID");
            AddForeignKey("dbo.Orders", "ticket_TicketId", "dbo.Tickets", "TicketId");
            DropColumn("dbo.Movies", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "ImageMimeType", c => c.String());
            DropForeignKey("dbo.Orders", "ticket_TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Seats", "Show_ShowID", "dbo.Shows");
            DropIndex("dbo.Seats", new[] { "Show_ShowID" });
            DropIndex("dbo.Orders", new[] { "ticket_TicketId" });
            AlterColumn("dbo.Pegis", "Racism", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "Drugs", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "Language", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "Sex", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "Horror", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "Vioence", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "SixteenPlus", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "TwelvePlus", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "SixPlus", c => c.Int(nullable: false));
            AlterColumn("dbo.Pegis", "All", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "ImageData", c => c.Binary());
            DropColumn("dbo.Shows", "StartTime");
            DropColumn("dbo.Seats", "Show_ShowID");
            DropColumn("dbo.Orders", "ticket_TicketId");
            DropTable("dbo.Tickets");
        }
    }
}
