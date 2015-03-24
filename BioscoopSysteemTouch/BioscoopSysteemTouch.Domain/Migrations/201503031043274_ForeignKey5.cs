namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class ForeignKey5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Name5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Name5", c => c.String());
        }
    }
}
