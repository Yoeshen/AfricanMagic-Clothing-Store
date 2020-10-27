namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteBOSalesDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BulkOrderSaleDetails", "BulkOrderImages_ID", "dbo.BulkOrderImages");
            DropForeignKey("dbo.BulkOrderSaleDetails", "BOSaleID", "dbo.BulkOrderSales");
            DropIndex("dbo.BulkOrderSaleDetails", new[] { "BOSaleID" });
            DropIndex("dbo.BulkOrderSaleDetails", new[] { "BulkOrderImages_ID" });
            DropTable("dbo.BulkOrderSaleDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BulkOrderSaleDetails",
                c => new
                    {
                        BOSaleDetailID = c.Int(nullable: false, identity: true),
                        BOSaleID = c.Int(nullable: false),
                        BOImageID = c.Int(nullable: false),
                        BOQuantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BulkOrderImages_ID = c.Int(),
                    })
                .PrimaryKey(t => t.BOSaleDetailID);
            
            CreateIndex("dbo.BulkOrderSaleDetails", "BulkOrderImages_ID");
            CreateIndex("dbo.BulkOrderSaleDetails", "BOSaleID");
            AddForeignKey("dbo.BulkOrderSaleDetails", "BOSaleID", "dbo.BulkOrderSales", "BOSaleID", cascadeDelete: true);
            AddForeignKey("dbo.BulkOrderSaleDetails", "BulkOrderImages_ID", "dbo.BulkOrderImages", "ID");
        }
    }
}
