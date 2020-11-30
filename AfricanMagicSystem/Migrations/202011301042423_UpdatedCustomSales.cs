namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCustomSales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomDesignSales", "TotalAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomDesignSales", "TotalAmount");
        }
    }
}
