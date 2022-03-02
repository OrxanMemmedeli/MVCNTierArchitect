namespace ShowcaseAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAnotherTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Adress = c.String(maxLength: 3000),
                        Email = c.String(maxLength: 3000),
                        Number = c.String(maxLength: 3000),
                        Map = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Developments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Heading = c.String(maxLength: 100),
                        Icon = c.String(maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 500),
                        Name = c.String(maxLength: 100),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SosialPages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        PageName = c.String(maxLength: 100),
                        URL = c.String(),
                        Icon = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Percent = c.Double(nullable: false),
                        Type = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tools");
            DropTable("dbo.SosialPages");
            DropTable("dbo.Images");
            DropTable("dbo.Developments");
            DropTable("dbo.Adresses");
            DropTable("dbo.Abouts");
        }
    }
}
