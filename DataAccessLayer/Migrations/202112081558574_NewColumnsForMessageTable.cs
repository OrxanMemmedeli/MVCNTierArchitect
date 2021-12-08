namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnsForMessageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsReaded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsResponded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "IsResponded");
            DropColumn("dbo.Messages", "IsReaded");
        }
    }
}
