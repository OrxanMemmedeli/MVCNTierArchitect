namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ContactID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ContactID");
        }
    }
}
