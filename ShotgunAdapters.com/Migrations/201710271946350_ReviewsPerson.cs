namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Person", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Person");
        }
    }
}
