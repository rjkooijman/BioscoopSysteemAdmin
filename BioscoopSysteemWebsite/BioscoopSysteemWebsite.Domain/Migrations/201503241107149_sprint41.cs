namespace BioscoopSysteemWebsite.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprint41 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        DirectorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DirectorID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        EnglishDescription = c.String(),
                        ImageData = c.String(),
                        BannerImage = c.String(),
                        Language = c.String(),
                        Subtitle = c.String(),
                        Duration = c.Int(nullable: false),
                        Type = c.Boolean(nullable: false),
                        Genre = c.String(),
                        TrailerUrl = c.String(),
                        IMDBUrl = c.String(),
                        FilmWebsite = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PegiId = c.Int(nullable: false),
                        DirectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Directors", t => t.DirectorID, cascadeDelete: true)
                .ForeignKey("dbo.Pegis", t => t.PegiId, cascadeDelete: true)
                .Index(t => t.PegiId)
                .Index(t => t.DirectorID);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        ActorName = c.String(),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.Pegis",
                c => new
                    {
                        PegiId = c.Int(nullable: false, identity: true),
                        All = c.Boolean(nullable: false),
                        SixPlus = c.Boolean(nullable: false),
                        TwelvePlus = c.Boolean(nullable: false),
                        SixteenPlus = c.Boolean(nullable: false),
                        Violence = c.Boolean(nullable: false),
                        Horror = c.Boolean(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        Language = c.Boolean(nullable: false),
                        Drugs = c.Boolean(nullable: false),
                        Racism = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PegiId);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        MailId = c.Int(nullable: false, identity: true),
                        MailAdres = c.String(),
                    })
                .PrimaryKey(t => t.MailId);
            
            CreateTable(
                "dbo.LadiesNights",
                c => new
                    {
                        LadiesNightid = c.Int(nullable: false, identity: true),
                        LadiesNightDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LadiesNightid);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Paid = c.Boolean(nullable: false),
                        PickedUp = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        ShowID = c.Int(nullable: false),
                        SecretShowOrder = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Shows", t => t.ShowID, cascadeDelete: true)
                .Index(t => t.ShowID);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        PopcornArrangement = c.Boolean(nullable: false),
                        VIP = c.Boolean(nullable: false),
                        MovieId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        LadiesNightid = c.Int(),
                        Holiday = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShowID)
                .ForeignKey("dbo.LadiesNights", t => t.LadiesNightid)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.RoomId)
                .Index(t => t.LadiesNightid);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        ShowID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Shows", t => t.ShowID, cascadeDelete: true)
                .Index(t => t.ShowID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNumber = c.Int(nullable: false),
                        Accessibility = c.Boolean(nullable: false),
                        Type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.RoomRows",
                c => new
                    {
                        RoomRowID = c.Int(nullable: false, identity: true),
                        RowNumber = c.Int(nullable: false),
                        SeatAmount = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomRowID)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        SeatId = c.Int(),
                        TicketSoortID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Seats", t => t.SeatId)
                .ForeignKey("dbo.TicketSoorts", t => t.TicketSoortID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.SeatId)
                .Index(t => t.TicketSoortID);
            
            CreateTable(
                "dbo.TicketSoorts",
                c => new
                    {
                        TicketSoortID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        EnglishTicketName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TicketSoortID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(nullable: false),
                        Role_RoleID = c.Int(nullable: false),
                        Role_Role = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ActorMovies",
                c => new
                    {
                        Actor_ActorId = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Actor_ActorId, t.Movie_MovieId })
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Actor_ActorId)
                .Index(t => t.Movie_MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Tickets", "TicketSoortID", "dbo.TicketSoorts");
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.Seats");
            DropForeignKey("dbo.Orders", "ShowID", "dbo.Shows");
            DropForeignKey("dbo.Shows", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomRows", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Shows", "LadiesNightid", "dbo.LadiesNights");
            DropForeignKey("dbo.Seats", "ShowID", "dbo.Shows");
            DropForeignKey("dbo.Movies", "PegiId", "dbo.Pegis");
            DropForeignKey("dbo.Movies", "DirectorID", "dbo.Directors");
            DropForeignKey("dbo.ActorMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.ActorMovies", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.ActorMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.ActorMovies", new[] { "Actor_ActorId" });
            DropIndex("dbo.Tickets", new[] { "TicketSoortID" });
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.RoomRows", new[] { "RoomId" });
            DropIndex("dbo.Seats", new[] { "ShowID" });
            DropIndex("dbo.Shows", new[] { "LadiesNightid" });
            DropIndex("dbo.Shows", new[] { "RoomId" });
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Orders", new[] { "ShowID" });
            DropIndex("dbo.Movies", new[] { "DirectorID" });
            DropIndex("dbo.Movies", new[] { "PegiId" });
            DropTable("dbo.ActorMovies");
            DropTable("dbo.Users");
            DropTable("dbo.TicketSoorts");
            DropTable("dbo.Tickets");
            DropTable("dbo.RoomRows");
            DropTable("dbo.Rooms");
            DropTable("dbo.Seats");
            DropTable("dbo.Shows");
            DropTable("dbo.Orders");
            DropTable("dbo.LadiesNights");
            DropTable("dbo.Mails");
            DropTable("dbo.Pegis");
            DropTable("dbo.Actors");
            DropTable("dbo.Movies");
            DropTable("dbo.Directors");
        }
    }
}
