namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewDateIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Reviews", "Date");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "Date" });
        }
    }
}
