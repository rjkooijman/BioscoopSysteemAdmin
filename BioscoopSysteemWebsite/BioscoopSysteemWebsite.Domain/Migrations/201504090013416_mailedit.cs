namespace BioscoopSysteemWebsite.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mailedit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "Voornaam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "Voornaam");
        }
    }
}
