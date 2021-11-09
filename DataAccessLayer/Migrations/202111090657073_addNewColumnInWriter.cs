namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumnInWriter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "About", c => c.String(maxLength: 100));
            AlterColumn("dbo.Writers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Writers", "Email", c => c.String(maxLength: 250));
            DropColumn("dbo.Writers", "About");
        }
    }
}
