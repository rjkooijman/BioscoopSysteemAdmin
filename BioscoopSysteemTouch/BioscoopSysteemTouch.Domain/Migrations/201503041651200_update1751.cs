namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public partial class update1751 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
