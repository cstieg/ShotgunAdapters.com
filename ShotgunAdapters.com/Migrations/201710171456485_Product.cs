namespace ShotgunAdapters.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GunCaliberId = c.Int(nullable: false),
                        AmmunitionCaliberId = c.Int(nullable: false),
                        ProductInfo = c.String(maxLength: 2000),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Shipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(),
                        ImageSrcSet = c.String(),
                        Category = c.String(maxLength: 50),
                        DisplayOnFrontPage = c.Boolean(nullable: false),
                        DoNotDisplay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Calibers", t => t.AmmunitionCaliberId, cascadeDelete: true)
                .ForeignKey("dbo.Calibers", t => t.GunCaliberId, cascadeDelete: true)
                .Index(t => t.GunCaliberId)
                .Index(t => t.AmmunitionCaliberId);
            
            CreateTable(
                "dbo.WebImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 200),
                        ImageSrcSet = c.String(maxLength: 1000),
                        Caption = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "GunCaliberId", "dbo.Calibers");
            DropForeignKey("dbo.Products", "AmmunitionCaliberId", "dbo.Calibers");
            DropIndex("dbo.WebImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "AmmunitionCaliberId" });
            DropIndex("dbo.Products", new[] { "GunCaliberId" });
            DropTable("dbo.WebImages");
            DropTable("dbo.Products");
        }
    }
}
