namespace AfricanMagicSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCustomerReviewsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerReviews", "SaleID", "dbo.Sales");
            DropIndex("dbo.CustomerReviews", new[] { "SaleID" });
            AddColumn("dbo.Sales", "CustomerReviews_CustomerReviewID", c => c.Int());
            AddColumn("dbo.CustomerReviews", "InvoiceNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerReviews", "Comment", c => c.String(nullable: false));
            CreateIndex("dbo.Sales", "CustomerReviews_CustomerReviewID");
            AddForeignKey("dbo.Sales", "CustomerReviews_CustomerReviewID", "dbo.CustomerReviews", "CustomerReviewID");
            DropColumn("dbo.CustomerReviews", "SaleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerReviews", "SaleID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sales", "CustomerReviews_CustomerReviewID", "dbo.CustomerReviews");
            DropIndex("dbo.Sales", new[] { "CustomerReviews_CustomerReviewID" });
            AlterColumn("dbo.CustomerReviews", "Comment", c => c.String());
            DropColumn("dbo.CustomerReviews", "InvoiceNumber");
            DropColumn("dbo.Sales", "CustomerReviews_CustomerReviewID");
            CreateIndex("dbo.CustomerReviews", "SaleID");
            AddForeignKey("dbo.CustomerReviews", "SaleID", "dbo.Sales", "SaleId", cascadeDelete: true);
        }
    }
}
