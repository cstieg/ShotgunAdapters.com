namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebImageProductId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebImages", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebImages", "ProductId");
        }
    }
}
