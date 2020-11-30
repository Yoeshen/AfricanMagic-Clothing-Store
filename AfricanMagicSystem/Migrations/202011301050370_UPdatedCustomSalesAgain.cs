namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedCustomSalesAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomDesignSales", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomDesignSales", "Quantity");
        }
    }
}
