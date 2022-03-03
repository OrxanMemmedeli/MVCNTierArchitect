namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CraateNewAPIAdressTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.APIAdresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.APIAdresses");
        }
    }
}
