namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Country : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsoCode2 = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ShippingCountries", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.ShippingCountries", "CountryId");
            AddForeignKey("dbo.ShippingCountries", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            DropColumn("dbo.ShippingCountries", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingCountries", "Country", c => c.String());
            DropForeignKey("dbo.ShippingCountries", "CountryId", "dbo.Countries");
            DropIndex("dbo.ShippingCountries", new[] { "CountryId" });
            DropColumn("dbo.ShippingCountries", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
