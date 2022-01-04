namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeColumnName : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ImageFiles");        
            DropColumn("dbo.ImageFiles", "MyProperty");
            AddColumn("dbo.ImageFiles", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ImageFiles", "ID");
   
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageFiles", "MyProperty", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ImageFiles");
            DropColumn("dbo.ImageFiles", "ID");
            AddPrimaryKey("dbo.ImageFiles", "MyProperty");
        }
    }
}
