namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationForRoleTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleMethods", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RoleMethods", "MethodNameID", "dbo.MethodNames");
            DropIndex("dbo.RoleMethods", new[] { "RoleID" });
            DropIndex("dbo.RoleMethods", new[] { "MethodNameID" });
            DropPrimaryKey("dbo.RoleMethods");
            AlterColumn("dbo.RoleMethods", "RoleID", c => c.Int(nullable: false));
            AlterColumn("dbo.RoleMethods", "MethodNameID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RoleMethods", new[] { "RoleID", "MethodNameID" });
            CreateIndex("dbo.RoleMethods", "RoleID");
            CreateIndex("dbo.RoleMethods", "MethodNameID");
            AddForeignKey("dbo.RoleMethods", "RoleID", "dbo.Roles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RoleMethods", "MethodNameID", "dbo.MethodNames", "ID", cascadeDelete: true);
            DropColumn("dbo.RoleMethods", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoleMethods", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.RoleMethods", "MethodNameID", "dbo.MethodNames");
            DropForeignKey("dbo.RoleMethods", "RoleID", "dbo.Roles");
            DropIndex("dbo.RoleMethods", new[] { "MethodNameID" });
            DropIndex("dbo.RoleMethods", new[] { "RoleID" });
            DropPrimaryKey("dbo.RoleMethods");
            AlterColumn("dbo.RoleMethods", "MethodNameID", c => c.Int());
            AlterColumn("dbo.RoleMethods", "RoleID", c => c.Int());
            AddPrimaryKey("dbo.RoleMethods", "ID");
            CreateIndex("dbo.RoleMethods", "MethodNameID");
            CreateIndex("dbo.RoleMethods", "RoleID");
            AddForeignKey("dbo.RoleMethods", "MethodNameID", "dbo.MethodNames", "ID");
            AddForeignKey("dbo.RoleMethods", "RoleID", "dbo.Roles", "ID");
        }
    }
}
