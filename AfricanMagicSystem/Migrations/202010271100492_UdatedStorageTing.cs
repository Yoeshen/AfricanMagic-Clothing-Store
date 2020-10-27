namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UdatedStorageTing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        StorageID = c.Int(nullable: false, identity: true),
                        BulkOrderImagesID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StorageID)
                .ForeignKey("dbo.BulkOrderImages", t => t.BulkOrderImagesID, cascadeDelete: true)
                .Index(t => t.BulkOrderImagesID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storages", "BulkOrderImagesID", "dbo.BulkOrderImages");
            DropIndex("dbo.Storages", new[] { "BulkOrderImagesID" });
            DropTable("dbo.Storages");
        }
    }
}
