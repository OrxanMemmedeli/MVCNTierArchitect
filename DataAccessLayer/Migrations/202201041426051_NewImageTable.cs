namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewImageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        MyProperty = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.MyProperty);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageFiles");
        }
    }
}
