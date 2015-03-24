namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class ForeignKeyMoviePegi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "PegiId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "PegiId");
            AddForeignKey("dbo.Movies", "PegiId", "dbo.Pegis", "PegiId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "PegiId", "dbo.Pegis");
            DropIndex("dbo.Movies", new[] { "PegiId" });
            DropColumn("dbo.Movies", "PegiId");
        }
    }
}
