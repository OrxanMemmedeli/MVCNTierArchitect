namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAPIAsdressTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.APIAdresses");
        }
        
        public override void Down()
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
    }
}
