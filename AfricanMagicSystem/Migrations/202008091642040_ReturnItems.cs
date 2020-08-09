namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnItems",
                c => new
                    {
                        ReturnCode = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        SaleID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        ReturnReason = c.String(),
                    })
                .PrimaryKey(t => t.ReturnCode)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.SaleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnItems", "SaleID", "dbo.Sales");
            DropForeignKey("dbo.ReturnItems", "ProductID", "dbo.Products");
            DropIndex("dbo.ReturnItems", new[] { "SaleID" });
            DropIndex("dbo.ReturnItems", new[] { "ProductID" });
            DropTable("dbo.ReturnItems");
        }
    }
}
