namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPoints : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "isExclusive");
            DropColumn("dbo.Products", "exclusivePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "exclusivePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "isExclusive", c => c.Boolean(nullable: false));
        }
    }
}
