namespace BioscoopSysteemWebsite.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprint42 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateIndex("dbo.Users", "RoleID");
            AddForeignKey("dbo.Users", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            DropColumn("dbo.Users", "Role_RoleID");
            DropColumn("dbo.Users", "Role_Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role_Role", c => c.String());
            AddColumn("dbo.Users", "Role_RoleID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropTable("dbo.Roles");
        }
    }
}
