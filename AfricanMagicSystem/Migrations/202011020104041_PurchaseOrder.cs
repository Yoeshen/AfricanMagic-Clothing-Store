namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        PurchaseOrderID = c.Int(nullable: false, identity: true),
                        ProductNeeded = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseOrderID);
            
            AddColumn("dbo.Products", "PurchaseOrder_PurchaseOrderID", c => c.Int());
            CreateIndex("dbo.Products", "PurchaseOrder_PurchaseOrderID");
            AddForeignKey("dbo.Products", "PurchaseOrder_PurchaseOrderID", "dbo.PurchaseOrders", "PurchaseOrderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "PurchaseOrder_PurchaseOrderID", "dbo.PurchaseOrders");
            DropIndex("dbo.Products", new[] { "PurchaseOrder_PurchaseOrderID" });
            DropColumn("dbo.Products", "PurchaseOrder_PurchaseOrderID");
            DropTable("dbo.PurchaseOrders");
        }
    }
}
