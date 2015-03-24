namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class migratiefrank : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Shows", "Name");
            DropColumn("dbo.Shows", "Language");
            DropColumn("dbo.Shows", "Subtitle");
            DropColumn("dbo.Shows", "Duration");
            DropColumn("dbo.Shows", "Type");
            DropColumn("dbo.Shows", "EndTime");
            DropColumn("dbo.Shows", "StartTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shows", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shows", "EndTime", c => c.DateTime(nullable: false));
        }
    }
}
