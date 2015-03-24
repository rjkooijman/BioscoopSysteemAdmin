namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class seat3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "PegiId", "dbo.Pegis");
            DropForeignKey("dbo.Orders", new[] { "MovieId", "RoomId" }, "dbo.Shows");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Shows", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Movies", new[] { "PegiId" });
            DropIndex("dbo.Orders", new[] { "MovieId", "RoomId" });
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Shows", new[] { "RoomId" });
            RenameColumn(table: "dbo.Movies", name: "PegiId", newName: "Pegi_PegiId");
            RenameColumn(table: "dbo.Orders", name: "MovieId", newName: "Show_ShowID");
            RenameColumn(table: "dbo.Shows", name: "MovieId", newName: "Movie_MovieId");
            RenameColumn(table: "dbo.Shows", name: "RoomId", newName: "Room_RoomId");
            DropPrimaryKey("dbo.Shows");
            AddColumn("dbo.Shows", "ShowID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Rooms", "SeatsAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "RowsAmount", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Pegi_PegiId", c => c.Int());
            AlterColumn("dbo.Orders", "Show_ShowID", c => c.Int());
            AlterColumn("dbo.Shows", "Movie_MovieId", c => c.Int());
            AlterColumn("dbo.Shows", "Room_RoomId", c => c.Int());
            AddPrimaryKey("dbo.Shows", "ShowID");
            CreateIndex("dbo.Movies", "Pegi_PegiId");
            CreateIndex("dbo.Orders", "Show_ShowID");
            CreateIndex("dbo.Shows", "Movie_MovieId");
            CreateIndex("dbo.Shows", "Room_RoomId");
            AddForeignKey("dbo.Movies", "Pegi_PegiId", "dbo.Pegis", "PegiId");
            AddForeignKey("dbo.Orders", "Show_ShowID", "dbo.Shows", "ShowID");
            AddForeignKey("dbo.Shows", "Movie_MovieId", "dbo.Movies", "MovieId");
            AddForeignKey("dbo.Shows", "Room_RoomId", "dbo.Rooms", "RoomId");
            DropColumn("dbo.Orders", "RoomId");
            DropColumn("dbo.Rooms", "Seats");
            DropColumn("dbo.Rooms", "Rows");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Rows", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "Seats", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "RoomId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Shows", "Room_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Shows", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.Orders", "Show_ShowID", "dbo.Shows");
            DropForeignKey("dbo.Movies", "Pegi_PegiId", "dbo.Pegis");
            DropIndex("dbo.Shows", new[] { "Room_RoomId" });
            DropIndex("dbo.Shows", new[] { "Movie_MovieId" });
            DropIndex("dbo.Orders", new[] { "Show_ShowID" });
            DropIndex("dbo.Movies", new[] { "Pegi_PegiId" });
            DropPrimaryKey("dbo.Shows");
            AlterColumn("dbo.Shows", "Room_RoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.Shows", "Movie_MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Show_ShowID", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Pegi_PegiId", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "RowsAmount");
            DropColumn("dbo.Rooms", "SeatsAmount");
            DropColumn("dbo.Shows", "ShowID");
            AddPrimaryKey("dbo.Shows", new[] { "MovieId", "RoomId" });
            RenameColumn(table: "dbo.Shows", name: "Room_RoomId", newName: "RoomId");
            RenameColumn(table: "dbo.Shows", name: "Movie_MovieId", newName: "MovieId");
            RenameColumn(table: "dbo.Orders", name: "Show_ShowID", newName: "MovieId");
            RenameColumn(table: "dbo.Movies", name: "Pegi_PegiId", newName: "PegiId");
            CreateIndex("dbo.Shows", "RoomId");
            CreateIndex("dbo.Shows", "MovieId");
            CreateIndex("dbo.Orders", new[] { "MovieId", "RoomId" });
            CreateIndex("dbo.Movies", "PegiId");
            AddForeignKey("dbo.Shows", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.Shows", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", new[] { "MovieId", "RoomId" }, "dbo.Shows", new[] { "MovieId", "RoomId" }, cascadeDelete: true);
            AddForeignKey("dbo.Movies", "PegiId", "dbo.Pegis", "PegiId", cascadeDelete: true);
        }
    }
}
