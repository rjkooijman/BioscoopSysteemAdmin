namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class ForeignKey3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Name5 = c.String(),
                        Description = c.String(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Language = c.String(),
                        Subtitle = c.String(),
                        Duration = c.Int(nullable: false),
                        Type = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketQuantity = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        PickedUp = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Pegis",
                c => new
                    {
                        PegiId = c.Int(nullable: false, identity: true),
                        All = c.Int(nullable: false),
                        SixPlus = c.Int(nullable: false),
                        TwelvePlus = c.Int(nullable: false),
                        SixteenPlus = c.Int(nullable: false),
                        Vioence = c.Int(nullable: false),
                        Horror = c.Int(nullable: false),
                        Sex = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Drugs = c.Int(nullable: false),
                        Racism = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PegiId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Seats = c.Int(nullable: false),
                        Rows = c.Int(nullable: false),
                        Accessibility = c.Boolean(nullable: false),
                        Type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
            DropTable("dbo.Pegis");
            DropTable("dbo.Orders");
            DropTable("dbo.Movies");
        }
    }
}
