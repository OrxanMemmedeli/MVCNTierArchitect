namespace ShowcaseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAdressTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Adresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
