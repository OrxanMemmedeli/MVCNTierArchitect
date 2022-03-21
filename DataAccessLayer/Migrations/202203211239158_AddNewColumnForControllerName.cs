namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnForControllerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ControllerNames", "Description", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ControllerNames", "Description");
        }
    }
}
