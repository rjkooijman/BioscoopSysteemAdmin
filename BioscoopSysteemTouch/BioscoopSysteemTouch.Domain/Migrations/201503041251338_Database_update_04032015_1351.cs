namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class Database_update_04032015_1351 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "RoomNumber");
        }
    }
}
