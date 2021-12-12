namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnForContactAndMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "DeletedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "IsDraft", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "DeletedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "DeletedDate");
            DropColumn("dbo.Messages", "IsDeleted");
            DropColumn("dbo.Messages", "IsDraft");
            DropColumn("dbo.Contacts", "DeletedDate");
            DropColumn("dbo.Contacts", "IsDeleted");
        }
    }
}
