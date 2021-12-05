namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnForContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "IsReaded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "IsResponded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "IsResponded");
            DropColumn("dbo.Contacts", "IsReaded");
            DropColumn("dbo.Contacts", "CreatedDate");
        }
    }
}
