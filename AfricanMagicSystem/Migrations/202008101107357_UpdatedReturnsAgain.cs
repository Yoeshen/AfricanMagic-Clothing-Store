namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReturnsAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReturnItems", "SaleID", "dbo.Sales");
            DropIndex("dbo.ReturnItems", new[] { "SaleID" });
            AddColumn("dbo.ReturnItems", "InvoiceNumber", c => c.Int(nullable: false));
            DropColumn("dbo.ReturnItems", "SaleID");
            DropColumn("dbo.ReturnItems", "ReturnDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReturnItems", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReturnItems", "SaleID", c => c.Int(nullable: false));
            DropColumn("dbo.ReturnItems", "InvoiceNumber");
            CreateIndex("dbo.ReturnItems", "SaleID");
            AddForeignKey("dbo.ReturnItems", "SaleID", "dbo.Sales", "SaleId", cascadeDelete: true);
        }
    }
}
