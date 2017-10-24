namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaliberDiameterPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Calibers", "Diameter", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Calibers", "Diameter", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
