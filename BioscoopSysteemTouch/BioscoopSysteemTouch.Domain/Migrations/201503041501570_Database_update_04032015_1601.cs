namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class Database_update_04032015_1601 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ticket_TicketId", "dbo.Tickets");
            DropIndex("dbo.Orders", new[] { "ticket_TicketId" });
            DropIndex("dbo.Seats", new[] { "Order_OrderId" });
            CreateTable(
                "dbo.TicketSoorts",
                c => new
                    {
                        TicketSoortID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TicketSoortID);
            
            AddColumn("dbo.Tickets", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tickets", "Seat_SeatId", c => c.Int());
            AddColumn("dbo.Tickets", "TicketSoort_TicketSoortID", c => c.Int());
            AddColumn("dbo.Tickets", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Tickets", "Seat_SeatId");
            CreateIndex("dbo.Tickets", "TicketSoort_TicketSoortID");
            CreateIndex("dbo.Tickets", "Order_OrderId");
            AddForeignKey("dbo.Tickets", "Seat_SeatId", "dbo.Seats", "SeatId");
            AddForeignKey("dbo.Tickets", "TicketSoort_TicketSoortID", "dbo.TicketSoorts", "TicketSoortID");
            AddForeignKey("dbo.Tickets", "Order_OrderId", "dbo.Orders", "OrderId");
            DropColumn("dbo.Orders", "Price");
            DropColumn("dbo.Orders", "TicketQuantity");
            DropColumn("dbo.Orders", "ticket_TicketId");
            DropColumn("dbo.Seats", "Order_OrderId");
            DropColumn("dbo.Tickets", "TicketSoort");
            DropColumn("dbo.Tickets", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tickets", "TicketSoort", c => c.Int(nullable: false));
            AddColumn("dbo.Seats", "Order_OrderId", c => c.Int());
            AddColumn("dbo.Orders", "ticket_TicketId", c => c.Int());
            AddColumn("dbo.Orders", "TicketQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Tickets", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Tickets", "TicketSoort_TicketSoortID", "dbo.TicketSoorts");
            DropForeignKey("dbo.Tickets", "Seat_SeatId", "dbo.Seats");
            DropIndex("dbo.Tickets", new[] { "Order_OrderId" });
            DropIndex("dbo.Tickets", new[] { "TicketSoort_TicketSoortID" });
            DropIndex("dbo.Tickets", new[] { "Seat_SeatId" });
            DropColumn("dbo.Tickets", "Order_OrderId");
            DropColumn("dbo.Tickets", "TicketSoort_TicketSoortID");
            DropColumn("dbo.Tickets", "Seat_SeatId");
            DropColumn("dbo.Tickets", "Price");
            DropTable("dbo.TicketSoorts");
            CreateIndex("dbo.Seats", "Order_OrderId");
            CreateIndex("dbo.Orders", "ticket_TicketId");
            AddForeignKey("dbo.Orders", "ticket_TicketId", "dbo.Tickets", "TicketId");
            AddForeignKey("dbo.Seats", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}
