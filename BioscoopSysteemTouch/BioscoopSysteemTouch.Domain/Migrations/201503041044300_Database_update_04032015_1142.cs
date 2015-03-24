namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class Database_update_04032015_1142 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomRows",
                c => new
                    {
                        RoomRowID = c.Int(nullable: false, identity: true),
                        RowNumber = c.Int(nullable: false),
                        SeatAmount = c.Int(nullable: false),
                        Room_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.RoomRowID)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId)
                .Index(t => t.Room_RoomId);
            
            DropColumn("dbo.Rooms", "SeatsAmount");
            DropColumn("dbo.Rooms", "RowsAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "RowsAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "SeatsAmount", c => c.Int(nullable: false));
            DropForeignKey("dbo.RoomRows", "Room_RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomRows", new[] { "Room_RoomId" });
            DropTable("dbo.RoomRows");
        }
    }
}
