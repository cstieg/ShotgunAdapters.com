namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewTitleLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Title", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Title", c => c.String(maxLength: 30));
        }
    }
}
