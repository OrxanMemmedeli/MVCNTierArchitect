namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumNameInHeading : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Headings", name: "CategorID", newName: "CategoryID");
            RenameIndex(table: "dbo.Headings", name: "IX_CategorID", newName: "IX_CategoryID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Headings", name: "IX_CategoryID", newName: "IX_CategorID");
            RenameColumn(table: "dbo.Headings", name: "CategoryID", newName: "CategorID");
        }
    }
}
