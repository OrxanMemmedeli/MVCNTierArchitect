namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnForMethodName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MethodNames", "Description", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MethodNames", "Description");
        }
    }
}
