namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTablesForRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(),
                        MethodNameID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MethodNames", t => t.MethodNameID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.MethodNameID);
            
            CreateTable(
                "dbo.MethodNames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Writers", "RoleID", c => c.Int());
            CreateIndex("dbo.Writers", "RoleID");
            AddForeignKey("dbo.Writers", "RoleID", "dbo.Roles", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Writers", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RoleMethods", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RoleMethods", "MethodNameID", "dbo.MethodNames");
            DropIndex("dbo.RoleMethods", new[] { "MethodNameID" });
            DropIndex("dbo.RoleMethods", new[] { "RoleID" });
            DropIndex("dbo.Writers", new[] { "RoleID" });
            DropColumn("dbo.Writers", "RoleID");
            DropTable("dbo.MethodNames");
            DropTable("dbo.RoleMethods");
            DropTable("dbo.Roles");
        }
    }
}
