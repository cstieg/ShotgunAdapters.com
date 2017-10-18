namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        Recipient = c.String(maxLength: 50),
                        Address1 = c.String(maxLength: 50),
                        Address2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        PostalCode = c.String(maxLength: 15),
                        Country = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        Registered = c.DateTime(nullable: false),
                        LastVisited = c.DateTime(nullable: false),
                        TimesVisited = c.Int(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Calibers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Diameter = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PlacedInCart = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Shipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        IsPromotionalItem = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.ProductBases", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        DateOrdered = c.DateTime(nullable: false),
                        ShipToAddressId = c.Int(nullable: false),
                        BillToAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.BillToAddressId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.ShipToAddressId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.ShipToAddressId)
                .Index(t => t.BillToAddressId);
            
            CreateTable(
                "dbo.ProductBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WebImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false, maxLength: 200),
                        ImageSrcSet = c.String(maxLength: 1000),
                        Caption = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        GunCaliberId = c.Int(nullable: false),
                        AmmunitionCaliberId = c.Int(nullable: false),
                        ProductInfo = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBases", t => t.Id)
                .ForeignKey("dbo.Calibers", t => t.GunCaliberId, cascadeDelete: true)
                .ForeignKey("dbo.Calibers", t => t.AmmunitionCaliberId, cascadeDelete: false)
                .Index(t => t.Id)
                .Index(t => t.GunCaliberId)
                .Index(t => t.AmmunitionCaliberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "AmmunitionCaliberId", "dbo.Calibers");
            DropForeignKey("dbo.Products", "GunCaliberId", "dbo.Calibers");
            DropForeignKey("dbo.Products", "Id", "dbo.ProductBases");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.ProductBases");
            DropForeignKey("dbo.Orders", "ShipToAddressId", "dbo.Addresses");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "BillToAddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "AmmunitionCaliberId" });
            DropIndex("dbo.Products", new[] { "GunCaliberId" });
            DropIndex("dbo.Products", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orders", new[] { "BillToAddressId" });
            DropIndex("dbo.Orders", new[] { "ShipToAddressId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropTable("dbo.Products");
            DropTable("dbo.WebImages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductBases");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Calibers");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
