namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingSchemes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShippingSchemeId = c.Int(nullable: false),
                        Country = c.String(),
                        MinQty = c.Int(),
                        MaxQty = c.Int(),
                        AdditionalShipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShippingSchemes", t => t.ShippingSchemeId, cascadeDelete: true)
                .Index(t => t.ShippingSchemeId);
            
            AddColumn("dbo.ProductBases", "ShippingSchemeId", c => c.Int());
            CreateIndex("dbo.ProductBases", "ShippingSchemeId");
            AddForeignKey("dbo.ProductBases", "ShippingSchemeId", "dbo.ShippingSchemes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductBases", "ShippingSchemeId", "dbo.ShippingSchemes");
            DropForeignKey("dbo.ShippingCountries", "ShippingSchemeId", "dbo.ShippingSchemes");
            DropIndex("dbo.ShippingCountries", new[] { "ShippingSchemeId" });
            DropIndex("dbo.ProductBases", new[] { "ShippingSchemeId" });
            DropColumn("dbo.ProductBases", "ShippingSchemeId");
            DropTable("dbo.ShippingCountries");
            DropTable("dbo.ShippingSchemes");
        }
    }
}
