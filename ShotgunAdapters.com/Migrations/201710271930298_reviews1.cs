namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviews1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Date", c => c.DateTime(nullable: false));
        }
    }
}
