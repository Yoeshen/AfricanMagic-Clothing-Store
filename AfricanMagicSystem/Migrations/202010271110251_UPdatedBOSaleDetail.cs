namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedBOSaleDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BulkOrderSaleDetails",
                c => new
                    {
                        BOSaleDetailID = c.Int(nullable: false, identity: true),
                        BOSaleID = c.Int(nullable: false),
                        BulkOrderImagesID = c.Int(nullable: false),
                        BOQuantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BOSaleDetailID)
                .ForeignKey("dbo.BulkOrderImages", t => t.BulkOrderImagesID, cascadeDelete: true)
                .ForeignKey("dbo.BulkOrderSales", t => t.BOSaleID, cascadeDelete: true)
                .Index(t => t.BOSaleID)
                .Index(t => t.BulkOrderImagesID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BulkOrderSaleDetails", "BOSaleID", "dbo.BulkOrderSales");
            DropForeignKey("dbo.BulkOrderSaleDetails", "BulkOrderImagesID", "dbo.BulkOrderImages");
            DropIndex("dbo.BulkOrderSaleDetails", new[] { "BulkOrderImagesID" });
            DropIndex("dbo.BulkOrderSaleDetails", new[] { "BOSaleID" });
            DropTable("dbo.BulkOrderSaleDetails");
        }
    }
}
