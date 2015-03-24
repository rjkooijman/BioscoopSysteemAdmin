namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class Test_FK2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "Pegi_PegiId", "dbo.Pegis");
            DropForeignKey("dbo.Orders", "Show_ShowID", "dbo.Shows");
            DropForeignKey("dbo.Tickets", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Seats", "Show_ShowID", "dbo.Shows");
            DropForeignKey("dbo.Shows", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Shows", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomRows", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Tickets", "TicketSoort_TicketSoortID", "dbo.TicketSoorts");
            DropIndex("dbo.Movies", new[] { "Pegi_PegiId" });
            DropIndex("dbo.Orders", new[] { "Show_ShowID" });
            DropIndex("dbo.Shows", new[] { "Movie_MovieId" });
            DropIndex("dbo.Shows", new[] { "Room_RoomId" });
            DropIndex("dbo.Seats", new[] { "Show_ShowID" });
            DropIndex("dbo.RoomRows", new[] { "Room_RoomId" });
            DropIndex("dbo.Tickets", new[] { "TicketSoort_TicketSoortID" });
            DropIndex("dbo.Tickets", new[] { "Order_OrderId" });
            RenameColumn(table: "dbo.Movies", name: "Pegi_PegiId", newName: "PegiId");
            RenameColumn(table: "dbo.Orders", name: "Show_ShowID", newName: "ShowID");
            RenameColumn(table: "dbo.Tickets", name: "Order_OrderId", newName: "OrderId");
            RenameColumn(table: "dbo.Seats", name: "Show_ShowID", newName: "ShowID");
            RenameColumn(table: "dbo.Shows", name: "Movie_MovieId", newName: "MovieId");
            RenameColumn(table: "dbo.Shows", name: "Room_RoomId", newName: "RoomId");
            RenameColumn(table: "dbo.RoomRows", name: "Room_RoomId", newName: "RoomId");
            RenameColumn(table: "dbo.Tickets", name: "Seat_SeatId", newName: "SeatId");
            RenameColumn(table: "dbo.Tickets", name: "TicketSoort_TicketSoortID", newName: "TicketSoortID");
            RenameIndex(table: "dbo.Tickets", name: "IX_Seat_SeatId", newName: "IX_SeatId");
            AlterColumn("dbo.Movies", "PegiId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ShowID", c => c.Int(nullable: false));
            AlterColumn("dbo.Shows", "MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Shows", "RoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.Seats", "ShowID", c => c.Int(nullable: false));
            AlterColumn("dbo.RoomRows", "RoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "TicketSoortID", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "PegiId");
            CreateIndex("dbo.Orders", "ShowID");
            CreateIndex("dbo.Shows", "MovieId");
            CreateIndex("dbo.Shows", "RoomId");
            CreateIndex("dbo.Seats", "ShowID");
            CreateIndex("dbo.RoomRows", "RoomId");
            CreateIndex("dbo.Tickets", "TicketSoortID");
            CreateIndex("dbo.Tickets", "OrderId");
            AddForeignKey("dbo.Movies", "PegiId", "dbo.Pegis", "PegiId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ShowID", "dbo.Shows", "ShowID", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.Seats", "ShowID", "dbo.Shows", "ShowID", cascadeDelete: true);
            AddForeignKey("dbo.Shows", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.Shows", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.RoomRows", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "TicketSoortID", "dbo.TicketSoorts", "TicketSoortID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketSoortID", "dbo.TicketSoorts");
            DropForeignKey("dbo.RoomRows", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Shows", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Seats", "ShowID", "dbo.Shows");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ShowID", "dbo.Shows");
            DropForeignKey("dbo.Movies", "PegiId", "dbo.Pegis");
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Tickets", new[] { "TicketSoortID" });
            DropIndex("dbo.RoomRows", new[] { "RoomId" });
            DropIndex("dbo.Seats", new[] { "ShowID" });
            DropIndex("dbo.Shows", new[] { "RoomId" });
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Orders", new[] { "ShowID" });
            DropIndex("dbo.Movies", new[] { "PegiId" });
            AlterColumn("dbo.Tickets", "OrderId", c => c.Int());
            AlterColumn("dbo.Tickets", "TicketSoortID", c => c.Int());
            AlterColumn("dbo.RoomRows", "RoomId", c => c.Int());
            AlterColumn("dbo.Seats", "ShowID", c => c.Int());
            AlterColumn("dbo.Shows", "RoomId", c => c.Int());
            AlterColumn("dbo.Shows", "MovieId", c => c.Int());
            AlterColumn("dbo.Orders", "ShowID", c => c.Int());
            AlterColumn("dbo.Movies", "PegiId", c => c.Int());
            RenameIndex(table: "dbo.Tickets", name: "IX_SeatId", newName: "IX_Seat_SeatId");
            RenameColumn(table: "dbo.Tickets", name: "TicketSoortID", newName: "TicketSoort_TicketSoortID");
            RenameColumn(table: "dbo.Tickets", name: "SeatId", newName: "Seat_SeatId");
            RenameColumn(table: "dbo.RoomRows", name: "RoomId", newName: "Room_RoomId");
            RenameColumn(table: "dbo.Shows", name: "RoomId", newName: "Room_RoomId");
            RenameColumn(table: "dbo.Shows", name: "MovieId", newName: "Movie_MovieId");
            RenameColumn(table: "dbo.Seats", name: "ShowID", newName: "Show_ShowID");
            RenameColumn(table: "dbo.Tickets", name: "OrderId", newName: "Order_OrderId");
            RenameColumn(table: "dbo.Orders", name: "ShowID", newName: "Show_ShowID");
            RenameColumn(table: "dbo.Movies", name: "PegiId", newName: "Pegi_PegiId");
            CreateIndex("dbo.Tickets", "Order_OrderId");
            CreateIndex("dbo.Tickets", "TicketSoort_TicketSoortID");
            CreateIndex("dbo.RoomRows", "Room_RoomId");
            CreateIndex("dbo.Seats", "Show_ShowID");
            CreateIndex("dbo.Shows", "Room_RoomId");
            CreateIndex("dbo.Shows", "Movie_MovieId");
            CreateIndex("dbo.Orders", "Show_ShowID");
            CreateIndex("dbo.Movies", "Pegi_PegiId");
            AddForeignKey("dbo.Tickets", "TicketSoort_TicketSoortID", "dbo.TicketSoorts", "TicketSoortID");
            AddForeignKey("dbo.RoomRows", "Room_RoomId", "dbo.Rooms", "RoomId");
            AddForeignKey("dbo.Shows", "Room_RoomId", "dbo.Rooms", "RoomId");
            AddForeignKey("dbo.Shows", "Movie_MovieId", "dbo.Movies", "MovieId");
            AddForeignKey("dbo.Seats", "Show_ShowID", "dbo.Shows", "ShowID");
            AddForeignKey("dbo.Tickets", "Order_OrderId", "dbo.Orders", "OrderId");
            AddForeignKey("dbo.Orders", "Show_ShowID", "dbo.Shows", "ShowID");
            AddForeignKey("dbo.Movies", "Pegi_PegiId", "dbo.Pegis", "PegiId");
        }
    }
}
