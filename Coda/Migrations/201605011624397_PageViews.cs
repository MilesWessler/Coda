namespace Coda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageViews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tabulatures", "PageViews", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tabulatures", "PageViews");
        }
    }
}
