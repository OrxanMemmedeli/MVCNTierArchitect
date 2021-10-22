namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Contacts", "Email", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(maxLength: 50));
            DropTable("dbo.TestTables");
        }
    }
}
