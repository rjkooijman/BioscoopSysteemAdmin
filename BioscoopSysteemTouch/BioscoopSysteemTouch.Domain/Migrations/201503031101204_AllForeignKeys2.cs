namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class AllForeignKeys2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Name = c.String(),
                        Language = c.String(),
                        Subtitle = c.String(),
                        Duration = c.Int(nullable: false),
                        Type = c.Boolean(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.RoomId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.RoomId);
            
            AddColumn("dbo.Orders", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", new[] { "MovieId", "RoomId" });
            AddForeignKey("dbo.Orders", new[] { "MovieId", "RoomId" }, "dbo.Shows", new[] { "MovieId", "RoomId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", new[] { "MovieId", "RoomId" }, "dbo.Shows");
            DropForeignKey("dbo.Shows", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropIndex("dbo.Shows", new[] { "RoomId" });
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Orders", new[] { "MovieId", "RoomId" });
            DropColumn("dbo.Orders", "RoomId");
            DropColumn("dbo.Orders", "MovieId");
            DropTable("dbo.Shows");
        }
    }
}
