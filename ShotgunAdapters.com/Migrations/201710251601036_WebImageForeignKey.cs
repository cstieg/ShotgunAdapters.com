namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebImageForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WebImages", "ProductId");
            AddForeignKey("dbo.WebImages", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebImages", "ProductId", "dbo.Products");
            DropIndex("dbo.WebImages", new[] { "ProductId" });
        }
    }
}
