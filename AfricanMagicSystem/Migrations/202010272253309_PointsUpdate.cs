namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PointsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isExclusive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "exclusivePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "exclusivePrice");
            DropColumn("dbo.Products", "isExclusive");
        }
    }
}
