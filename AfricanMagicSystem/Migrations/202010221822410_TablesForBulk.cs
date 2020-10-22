namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesForBulk : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.BOSaleDetailID)
                .ForeignKey("dbo.BulkOrderImages", t => t.BulkOrderImages_ID)
                .ForeignKey("dbo.BulkOrderSales", t => t.BOSaleID, cascadeDelete: true)
                .Index(t => t.BOSaleID)
                .Index(t => t.BulkOrderImages_ID);
            
            CreateTable(
                "dbo.BulkOrderSales",
                c => new
                    {
                        BOSaleID = c.Int(nullable: false, identity: true),
                        BOSaleDate = c.DateTime(nullable: false),
                        BOEmail = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BOStatus = c.String(),
                        BOPhoneNumber = c.String(),
                        AdditionalNotes = c.String(),
                    })
                .PrimaryKey(t => t.BOSaleID);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        PKey = c.Int(nullable: false, identity: true),
                        BOImageID = c.Int(nullable: false),
                        BulkOrderImages_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PKey)
                .ForeignKey("dbo.BulkOrderImages", t => t.BulkOrderImages_ID)
                .Index(t => t.BulkOrderImages_ID);
            
            AddColumn("dbo.BulkOrderImages", "Size", c => c.String());
            AddColumn("dbo.BulkOrderImages", "Colour", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storages", "BulkOrderImages_ID", "dbo.BulkOrderImages");
            DropForeignKey("dbo.BulkOrderSaleDetails", "BOSaleID", "dbo.BulkOrderSales");
            DropForeignKey("dbo.BulkOrderSaleDetails", "BulkOrderImages_ID", "dbo.BulkOrderImages");
            DropIndex("dbo.Storages", new[] { "BulkOrderImages_ID" });
            DropIndex("dbo.BulkOrderSaleDetails", new[] { "BulkOrderImages_ID" });
            DropIndex("dbo.BulkOrderSaleDetails", new[] { "BOSaleID" });
            DropColumn("dbo.BulkOrderImages", "Colour");
            DropColumn("dbo.BulkOrderImages", "Size");
            DropTable("dbo.Storages");
            DropTable("dbo.BulkOrderSales");
            DropTable("dbo.BulkOrderSaleDetails");
        }
    }
}
