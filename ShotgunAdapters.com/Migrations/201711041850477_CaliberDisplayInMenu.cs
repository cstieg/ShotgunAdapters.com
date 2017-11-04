namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaliberDisplayInMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calibers", "DisplayInMenu", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Calibers", "DisplayInMenu");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Calibers", new[] { "DisplayInMenu" });
            DropColumn("dbo.Calibers", "DisplayInMenu");
        }
    }
}
