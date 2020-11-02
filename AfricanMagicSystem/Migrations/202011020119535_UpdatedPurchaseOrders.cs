namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPurchaseOrders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseOrders", "ProductNeeded", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrders", "ProductNeeded", c => c.Int(nullable: false));
        }
    }
}
