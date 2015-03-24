namespace BioscoopSysteemTouch.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    
    public partial class Database_update_04032015_1603 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketSoorts", "Naam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketSoorts", "Naam");
        }
    }
}
