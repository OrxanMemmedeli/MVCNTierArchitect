namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAdminUserNameColumnLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "UserName", c => c.String(maxLength: 50));
        }
    }
}
