namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebImages", "Order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebImages", "Order");
        }
    }
}
