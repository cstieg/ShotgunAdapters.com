namespace ShotgunAdapters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeProductCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductBases", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductBases", "Category", c => c.String(maxLength: 50));
        }
    }
}
