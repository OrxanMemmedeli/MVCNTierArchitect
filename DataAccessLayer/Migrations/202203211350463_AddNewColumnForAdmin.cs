namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnForAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "RoleID", c => c.Int());
            CreateIndex("dbo.Admins", "RoleID");
            AddForeignKey("dbo.Admins", "RoleID", "dbo.Roles", "ID");
            DropColumn("dbo.Admins", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Role", c => c.String(maxLength: 1));
            DropForeignKey("dbo.Admins", "RoleID", "dbo.Roles");
            DropIndex("dbo.Admins", new[] { "RoleID" });
            DropColumn("dbo.Admins", "RoleID");
        }
    }
}
