namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveWebImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductBases", "ProductInfo", c => c.String(maxLength: 2000));
            DropColumn("dbo.ProductBases", "Description");
            DropColumn("dbo.ProductBases", "ImageUrl");
            DropColumn("dbo.ProductBases", "ImageSrcSet");
            DropColumn("dbo.Products", "ProductInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductInfo", c => c.String(maxLength: 2000));
            AddColumn("dbo.ProductBases", "ImageSrcSet", c => c.String());
            AddColumn("dbo.ProductBases", "ImageUrl", c => c.String());
            AddColumn("dbo.ProductBases", "Description", c => c.String(maxLength: 100));
            DropColumn("dbo.ProductBases", "ProductInfo");
        }
    }
}
