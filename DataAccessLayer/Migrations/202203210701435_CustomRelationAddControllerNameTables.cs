namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomRelationAddControllerNameTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleControllerNames",
                c => new
                    {
                        RoleID = c.Int(nullable: false),
                        ControllerNameID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleID, t.ControllerNameID })
                .ForeignKey("dbo.ControllerNames", t => t.ControllerNameID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.ControllerNameID);
            
            CreateTable(
                "dbo.ControllerNames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleControllerNames", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RoleControllerNames", "ControllerNameID", "dbo.ControllerNames");
            DropIndex("dbo.RoleControllerNames", new[] { "ControllerNameID" });
            DropIndex("dbo.RoleControllerNames", new[] { "RoleID" });
            DropTable("dbo.ControllerNames");
            DropTable("dbo.RoleControllerNames");
        }
    }
}
