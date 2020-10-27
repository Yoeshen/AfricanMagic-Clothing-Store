namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletdTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Storages", "BulkOrderImages_ID", "dbo.BulkOrderImages");
            DropIndex("dbo.Storages", new[] { "BulkOrderImages_ID" });
            DropTable("dbo.Storages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        PKey = c.Int(nullable: false, identity: true),
                        BOImageID = c.Int(nullable: false),
                        BulkOrderImages_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PKey);
            
            CreateIndex("dbo.Storages", "BulkOrderImages_ID");
            AddForeignKey("dbo.Storages", "BulkOrderImages_ID", "dbo.BulkOrderImages", "ID");
        }
    }
}
