namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration0940 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropIndex("dbo.Shows", new[] { "MovieId" });
            RenameColumn(table: "dbo.Shows", name: "MovieId", newName: "Movie_MovieId");
            AddColumn("dbo.Shows", "PopcornArrangement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Shows", "Movie_MovieId", c => c.Int());
            CreateIndex("dbo.Shows", "Movie_MovieId");
            AddForeignKey("dbo.Shows", "Movie_MovieId", "dbo.Movies", "MovieId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shows", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.Shows", new[] { "Movie_MovieId" });
            AlterColumn("dbo.Shows", "Movie_MovieId", c => c.Int(nullable: false));
            DropColumn("dbo.Shows", "PopcornArrangement");
            RenameColumn(table: "dbo.Shows", name: "Movie_MovieId", newName: "MovieId");
            CreateIndex("dbo.Shows", "MovieId");
            AddForeignKey("dbo.Shows", "MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
        }
    }
}
