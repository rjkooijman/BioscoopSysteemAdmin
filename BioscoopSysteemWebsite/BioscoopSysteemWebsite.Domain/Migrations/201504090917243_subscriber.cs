namespace BioscoopSysteemWebsite.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subscriber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        SubscriberId = c.Int(nullable: false, identity: true),
                        Voornaam = c.String(nullable: false),
                        Achternaam = c.String(nullable: false),
                        Straat = c.String(nullable: false),
                        Huisnummer = c.Int(nullable: false),
                        Postcode = c.String(nullable: false),
                        Woonplaats = c.String(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.SubscriberId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscribers");
        }
    }
}
