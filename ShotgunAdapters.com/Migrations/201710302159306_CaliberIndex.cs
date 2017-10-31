namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaliberIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Calibers", "Diameter");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Calibers", new[] { "Diameter" });
        }
    }
}
