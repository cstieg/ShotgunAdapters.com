namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebImageNullProduct : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.WebImages", new[] { "ProductId" });
            AlterColumn("dbo.WebImages", "ProductId", c => c.Int());
            CreateIndex("dbo.WebImages", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.WebImages", new[] { "ProductId" });
            AlterColumn("dbo.WebImages", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.WebImages", "ProductId");
        }
    }
}
